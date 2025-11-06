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

    public async Task<List<TtsModel>> GetAllAsync()
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

    public async Task<TtsModel> CreateAsync(TtsModel model)
    {
        model.ModelId = Guid.NewGuid();
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;

        _db.TtsModels.Add(model);
        await _db.SaveChangesAsync();
        return model;
    }

    public async Task<bool> UpdateAsync(Guid id, TtsModel updated)
    {
        var existing = await _db.TtsModels.FindAsync(id);
        if (existing is null) return false;

        existing.Name = updated.Name;
        existing.Provider = updated.Provider;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await _db.TtsModels.FindAsync(id);
        if (model is null) return false;

        _db.TtsModels.Remove(model);
        await _db.SaveChangesAsync();
        return true;
    }
}
