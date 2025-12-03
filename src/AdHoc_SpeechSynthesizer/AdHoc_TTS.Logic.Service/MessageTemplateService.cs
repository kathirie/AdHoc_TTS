using AdHoc_SpeechSynthesizer.Common.Templating;
using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;



namespace AdHoc_SpeechSynthesizer.Services;

public class MessageTemplateService(IMessageTemplateDao dao) : IMessageTemplateService
{
    private readonly IMessageTemplateDao _dao = dao ?? throw new ArgumentNullException(nameof(dao));

    public async Task<IEnumerable<MessageTemplate>> GetAllAsync()
        => await _dao.FindAllAsync();

    public async Task<MessageTemplate?> GetByIdAsync(Guid id)
        => await _dao.FindByIdAsync(id);

    public async Task<IEnumerable<TemplatePlaceholder?>> GetPlaceholdersByIdAsync(Guid id)
    {
        var template = await _dao.FindByIdAsync(id);
        if (template is null)
            return Enumerable.Empty<TemplatePlaceholder>();

        var names = TemplatePlaceholderScanner.GetPlaceholders(template.SsmlContent);
        return names.Select(n => new TemplatePlaceholder { Name = n });
    }
}
