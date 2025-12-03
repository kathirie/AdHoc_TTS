using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado;

public class TtsModelDao(AppDbContext db) : ITtsModelDao
{
    private readonly AppDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

    public async Task<IEnumerable<TtsModel>> FindAllAsync()
    {
        return await _db.TtsModels
            .AsNoTracking()
            .OrderBy(m => m.Provider)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<TtsModel?> FindByIdAsync(Guid id)
    {
        return await _db.TtsModels
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ModelId == id);
    }
}
