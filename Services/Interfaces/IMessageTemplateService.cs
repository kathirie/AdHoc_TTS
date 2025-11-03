using AdHoc_SpeechSynthesizer.Models;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces
{
    public interface IMessageTemplateService
    {
        Task<List<MessageTemplate>> GetAllAsync();
        Task<MessageTemplate?> GetByIdAsync(Guid id);
    }
}
