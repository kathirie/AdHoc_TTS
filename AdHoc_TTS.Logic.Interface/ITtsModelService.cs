using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface ITtsModelService
{
    Task<IEnumerable<TtsModel>> GetAllAsync();
    Task<TtsModel?> GetByIdAsync(Guid id);

}
