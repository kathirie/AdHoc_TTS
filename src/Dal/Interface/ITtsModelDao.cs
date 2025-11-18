using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Dal.Interface;

public interface ITtsModelDao
{
    Task<IEnumerable<TtsModel>> FindAllAsync();
    Task<TtsModel?> FindByIdAsync(Guid id);
}
