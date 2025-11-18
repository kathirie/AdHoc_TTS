using AdHoc_SpeechSynthesizer.Common.Templating;
using AdHoc_SpeechSynthesizer.Common.Validation;
using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Models.Requests;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using AdHoc_SpeechSynthesizer.Services.Synthesizers;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services;

public class SynthesisService : ISynthesisService
{
    private readonly ITtsModelDao _modelDao;
    private readonly ITtsVoiceDao _voiceDao;
    private readonly IMessageTemplateDao _templateDao;
    private readonly IConfiguration _config;
    private readonly SsmlValidator _validator;

    public SynthesisService(
        ITtsModelDao modelDao,
        ITtsVoiceDao voiceDao,
        IMessageTemplateDao templateDao,
        IConfiguration config,
        SsmlValidator validator)
    {
        _modelDao = modelDao;
        _voiceDao = voiceDao;
        _templateDao = templateDao;
        _config = config;
        _validator = validator;
    }

    // SSML → WAV
    public async Task<byte[]> SynthesizeAsync(SynthesisRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.SsmlContent))
            throw new ArgumentException("SSML content cannot be empty.");

        // SSML validation
        var validation = _validator.Validate(req.SsmlContent);
        if (!validation.IsXmlWellFormed || !validation.IsSchemaValid)
        {
            var errors = string.Join(Environment.NewLine, validation.Errors);
            throw new InvalidOperationException($"Rendered SSML is not valid.\n{errors}");
        }

        // get model
        var model = await _modelDao.FindByIdAsync(req.ModelId) ?? throw new ArgumentException("Invalid TTS model ID.");

        // get voice
        TtsVoice? voice;

        if (req.VoiceId != null)
        {
            voice = await _voiceDao.FindByIdAsync(req.VoiceId.Value);
        }
        else
        {
            voice = await _voiceDao.FindDefaultForModelAsync(model.ModelId);
        }

        if (voice is null)
            throw new InvalidOperationException("No valid voice found for selected model.");

        // select synthesizer
        ITtsSynthesizer synthesizer = model.Provider switch
        {
            "azure" => new AzureSynthesizer(
                _config["SPEECH_KEY"] ?? throw new InvalidOperationException("Missing Azure key"),
                _config["SPEECH_REGION"] ?? throw new InvalidOperationException("Missing Azure region"),
                voice.ProviderVoiceId),

            "system.speech" => new WindowsSynthesizer(voice.ProviderVoiceId),

            _ => throw new NotSupportedException($"Unknown model provider '{model.Provider}'.")
        };

        try
        {
            // SSML → WAV
            var wav = await synthesizer.SynthesizeToWavAsync(req.SsmlContent);
            return wav;
        }
        finally
        {
            synthesizer.Dispose();
        }
    }

    // Template → SSML → WAV
    public async Task<byte[]> SynthesizeFromTemplateAsync(SynthesizeFromTemplateRequest req)
    {
        // load template
        var template = await _templateDao.FindByIdAsync(req.TemplateId) ?? throw new ArgumentException($"MessageTemplate with id '{req.TemplateId}' not found.");

        // prepare placeholders
        var sequenceValues = new Dictionary<string, IEnumerable<string>>();

        if (req.RefLocationNames?.Any() == true)
            sequenceValues["Location.RefLocationName"] = req.RefLocationNames!;

        if (req.PlatformNumbers?.Any() == true)
            sequenceValues["Platform.PlatformNr"] = req.PlatformNumbers!
                .Select(n => n?.ToString() ?? string.Empty)
                .ToList();

        if (req.RouteNrs?.Any() == true)
            sequenceValues["Route.RouteNr"] = req.RouteNrs!
                .Select(n => n?.ToString() ?? string.Empty)
                .ToList();

        if (req.FrontTexts?.Any() == true)
            sequenceValues["TargetText.FrontText"] = req.FrontTexts!;

        if (req.FreeTexts?.Any() == true)
            sequenceValues["Freitext"] = req.FreeTexts!;

        // render template
        var ssml = TemplateRenderer.Render(template.SsmlContent, sequenceValues);

        // SSML → WAV
        var synthesisReq = new SynthesisRequest
        {
            ModelId = req.ModelId,
            VoiceId = req.VoiceId,
            SsmlContent = ssml
        };

        return await SynthesizeAsync(synthesisReq);
    }
}