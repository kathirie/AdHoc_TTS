using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.AppContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.EntityFrameworkCore;


namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public class MessageTemplateService : IMessageTemplateService
{
    private readonly AppDbContext _db;

    public MessageTemplateService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<MessageTemplate>> GetAllAsync()
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
}
