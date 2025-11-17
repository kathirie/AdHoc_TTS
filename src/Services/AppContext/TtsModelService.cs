using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.AppContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public class TtsModelService : ITtsModelService
{
    private readonly AppDbContext _db;

    public TtsModelService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<TtsModel>> GetAllAsync()
    {
        return await _db.TtsModels
            .AsNoTracking()
            .OrderBy(m => m.Provider)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<TtsModel?> GetByIdAsync(Guid id)
    {
        return await _db.TtsModels
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ModelId == id);
    }
}
