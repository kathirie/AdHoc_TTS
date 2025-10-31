using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.Requests;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using AdHoc_SpeechSynthesizer.Services.Synthesizers;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services
{
    public class SynthesisService : ISynthesisService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public SynthesisService(AppDbContext db, IConfiguration config, IServiceProvider serviceProvider)
        {
            _db = db;
            _config = config;
            _serviceProvider = serviceProvider;
        }

        public async Task<byte[]> SynthesizeAsync(SynthesisRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.SsmlContent))
                throw new ArgumentException("SSML content cannot be empty.");

            // 1. Load model
            var model = await _db.TtsModels.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ModelId == req.ModelId);

            if (model is null)
                throw new ArgumentException("Invalid TTS model ID.");

            // 2. Load or fallback to default voice
            var voice = await _db.TtsVoices.AsNoTracking()
                .FirstOrDefaultAsync(v =>
                    (req.VoiceId != null && v.VoiceId == req.VoiceId) ||
                    (req.VoiceId == null && v.ModelId == model.ModelId && v.IsActive));

            if (voice is null)
                throw new InvalidOperationException("No valid voice found for selected model.");

            // 3. Select synthesizer implementation via interface
            ITtsSynthesizer synthesizer = model.Provider switch
            {
                "azure" => new AzureSynthesizer(
                    _config["SPEECH_KEY"] ?? throw new InvalidOperationException("Missing Azure key"),
                    _config["SPEECH_REGION"] ?? throw new InvalidOperationException("Missing Azure region"),
                    voice.ProviderVoiceId),

                "system.speech" => new WindowsSynthesizer(voice.ProviderVoiceId),

                _ => throw new NotSupportedException($"Unknown model provider '{model.Provider}'.")
            };

            // 4. Synthesize
            var wav = await synthesizer.SynthesizeToWavAsync(req.SsmlContent);
            synthesizer.Dispose();

            return wav;
        }
    }
}
