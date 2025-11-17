using AdHoc_SpeechSynthesizer.Models.AppContext;
using AdHoc_SpeechSynthesizer.Models.Responses;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface IMessageTemplateService
{
    Task<IEnumerable<MessageTemplate>> GetAllAsync();
    Task<MessageTemplate?> GetByIdAsync(Guid id);
    Task<IEnumerable<TemplatePlaceholderResponse?>> GetPlaceholdersByIdAsync(Guid id);
}
