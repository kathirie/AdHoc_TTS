using AdHoc_SpeechSynthesizer.Models;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces
{
    public interface ITtsVoiceService
    {
        Task<List<TtsVoice>> GetAllAsync(string? locale = null, string? provider = null);
        Task<TtsVoice?> GetByIdAsync(Guid id);
    }
}
