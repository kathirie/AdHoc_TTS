using AdHoc_SpeechSynthesizer.Models.AppContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface ITtsVoiceService
{
    Task<IEnumerable<TtsVoice>> GetAllAsync(string? locale = null, string? provider = null);
    Task<TtsVoice?> GetByIdAsync(Guid id);
}
