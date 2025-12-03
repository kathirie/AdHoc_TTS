using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Dal.Interface
{
    public interface ITtsVoiceDao
    {
        Task<IEnumerable<TtsVoice>> FindAllAsync(string? locale = null, string? provider = null);
        Task<TtsVoice?> FindByIdAsync(Guid id);
        Task<TtsVoice?> FindDefaultForModelAsync(Guid modelId);
    }
}
