using AdHoc_SpeechSynthesizer.Models;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces
{
    public interface ITtsModelService
    {
        Task<List<TtsModel>> GetAllAsync();
        Task<TtsModel?> GetByIdAsync(Guid id);
        Task<TtsModel> CreateAsync(TtsModel model);
        Task<bool> UpdateAsync(Guid id, TtsModel updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
