using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Helpers;
using AdHoc_SpeechSynthesizer.Helpers.Validation;

using AdHoc_SpeechSynthesizer.Models.Requests;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using AdHoc_SpeechSynthesizer.Services.Synthesizers;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services;

public class SynthesisService : ISynthesisService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    private readonly IServiceProvider _serviceProvider;
    private readonly SsmlValidator _validator;

    public SynthesisService(
        AppDbContext db,
        IConfiguration config,
        IServiceProvider serviceProvider,
        SsmlValidator validator)
    {
        _db = db;
        _config = config;
        _serviceProvider = serviceProvider;
        _validator = validator;
    }

    // raw SSML → WAV
    public async Task<byte[]> SynthesizeAsync(SynthesisRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.SsmlContent))
            throw new ArgumentException("SSML content cannot be empty.");

        // Validate SSML
        var validation = _validator.Validate(req.SsmlContent);
        if (!validation.IsXmlWellFormed || !validation.IsSchemaValid)
        {
            var errors = string.Join(Environment.NewLine, validation.Errors);
            throw new InvalidOperationException(
                $"Rendered SSML is not valid.\n{errors}");
        }

        // Load model
        var model = await _db.TtsModels.AsNoTracking()
            .FirstOrDefaultAsync(m => m.ModelId == req.ModelId);

        if (model is null)
            throw new ArgumentException("Invalid TTS model ID.");

        // Load or fallback to default voice
        var voice = await _db.TtsVoices.AsNoTracking()
            .FirstOrDefaultAsync(v =>
                (req.VoiceId != null && v.VoiceId == req.VoiceId) ||
                (req.VoiceId == null && v.ModelId == model.ModelId && v.IsActive));

        if (voice is null)
            throw new InvalidOperationException("No valid voice found for selected model.");


        // Select synthesizer implementation via interface
        ITtsSynthesizer synthesizer = model.Provider switch
        {
            "azure" => new AzureSynthesizer(
                _config["SPEECH_KEY"] ?? throw new InvalidOperationException("Missing Azure key"),
                _config["SPEECH_REGION"] ?? throw new InvalidOperationException("Missing Azure region"),
                voice.ProviderVoiceId),

            "system.speech" => new WindowsSynthesizer(voice.ProviderVoiceId),

            _ => throw new NotSupportedException($"Unknown model provider '{model.Provider}'.")
        };

        // Synthesize
        var wav = await synthesizer.SynthesizeToWavAsync(req.SsmlContent);
        synthesizer.Dispose();

        return wav;
    }

    // Template → SSML → reuse Synthesize from SSML
    public async Task<byte[]> SynthesizeFromTemplateAsync(SynthesizeFromTemplateRequest req)
    {
        // Load MessageTemplate from AppDbContext by GUID
        var template = await _db.MessageTemplates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TemplateId == req.TemplateId);

        if (template == null)
            throw new ArgumentException($"MessageTemplate with id '{req.TemplateId}' not found.");

        // Build placeholder dictionary from request
        var values = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(req.RefLocationName))
            values["Location.RefLocationName"] = req.RefLocationName;

        if (!string.IsNullOrWhiteSpace(req.PlatformName))
            values["Platform.Name"] = req.PlatformName;

        if (req.RouteNr.HasValue)
            values["Route.RouteNr"] = req.RouteNr.Value.ToString();

        if (!string.IsNullOrWhiteSpace(req.FrontText))
            values["TargetText.FrontText"] = req.FrontText;

        if (!string.IsNullOrWhiteSpace(req.FreeText))
            values["Freitext"] = req.FreeText;

        // Render SSML from template.SsmlContent
        var ssml = TemplateRenderer.Render(template.SsmlContent, values);

        // using Sythesise from SSML
        var synthesisReq = new SynthesisRequest
        {
            ModelId = req.ModelId,
            VoiceId = req.VoiceId,
            SsmlContent = ssml
        };

        return await SynthesizeAsync(synthesisReq);
    }
}