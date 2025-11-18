using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface ITtsVoiceService
{
    Task<IEnumerable<TtsVoice>> GetAllAsync(string? locale = null, string? provider = null);
    Task<TtsVoice?> GetByIdAsync(Guid id);
}
