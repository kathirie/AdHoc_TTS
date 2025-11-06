using AdHoc_SpeechSynthesizer.Models.AppContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface IMessageTemplateService
{
    Task<List<MessageTemplate>> GetAllAsync();
    Task<MessageTemplate?> GetByIdAsync(Guid id);
}
