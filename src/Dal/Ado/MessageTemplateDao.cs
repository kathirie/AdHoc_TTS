using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado;

public class MessageTemplateDao : IMessageTemplateDao
{
    private readonly AppDbContext _db;

    public MessageTemplateDao(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<MessageTemplate>> FindAllAsync()
    {
        return await _db.MessageTemplates
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<MessageTemplate?> FindByIdAsync(Guid id)
    {
        return await _db.MessageTemplates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TemplateId == id);
    }
}
