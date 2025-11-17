using AdHoc_SpeechSynthesizer.Common.Templating;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.AppContext;
using AdHoc_SpeechSynthesizer.Models.Responses;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;


namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public class MessageTemplateService : IMessageTemplateService
{
    private readonly AppDbContext _db;

    public MessageTemplateService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<MessageTemplate>> GetAllAsync()
    {
        return await _db.MessageTemplates
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<MessageTemplate?> GetByIdAsync(Guid id)
    {
        return await _db.MessageTemplates
                        .AsNoTracking()
                        .FirstOrDefaultAsync(v => v.TemplateId == id);
    }

    public async Task<IEnumerable<TemplatePlaceholderResponse?>> GetPlaceholdersByIdAsync(Guid id)
    {
        var template = await GetByIdAsync(id);
        if (template is null)
        {
            return null; 
        }

        var placeholderNames = TemplatePlaceholderScanner.GetPlaceholders(template.SsmlContent);

        return placeholderNames
        .Select(name => new TemplatePlaceholderResponse { Name = name })
        .ToList();
    }
}
