
using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

public interface IMessageTemplateService
{
    Task<IEnumerable<MessageTemplate>> GetAllAsync();
    Task<MessageTemplate> GetByIdAsync(Guid id);
    Task<IEnumerable<TemplatePlaceholder?>> GetPlaceholdersByIdAsync(Guid id);
}
