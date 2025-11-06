using AdHoc_SpeechSynthesizer.Models.AppContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface ITtsModelService
{
    Task<List<TtsModel>> GetAllAsync();
    Task<TtsModel?> GetByIdAsync(Guid id);
    Task<TtsModel> CreateAsync(TtsModel model);
    Task<bool> UpdateAsync(Guid id, TtsModel updated);
    Task<bool> DeleteAsync(Guid id);
}
