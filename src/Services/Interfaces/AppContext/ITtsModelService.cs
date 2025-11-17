using AdHoc_SpeechSynthesizer.Models.AppContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface ITtsModelService
{
    Task<IEnumerable<TtsModel>> GetAllAsync();
    Task<TtsModel?> GetByIdAsync(Guid id);

}
