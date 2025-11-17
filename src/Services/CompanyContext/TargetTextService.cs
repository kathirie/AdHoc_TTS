using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.CompanyContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.CompanyContext;

public class TargetTextService : ITargetTextService
{
    private readonly CompanyDbContext _db;

    public TargetTextService(CompanyDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<TargetText>> GetAllAsync()
    {
        var query = _db.TargetTexts.AsNoTracking().AsQueryable();

        return await query
            .OrderBy(t => t.ControlCenterId)
            .ThenBy(t => t.VersionNr)
            .ThenBy(t => t.TargetTextNr)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAllFrontTextsAsync()
    {
        return await _db.TargetTexts
            .AsNoTracking()
            .Where(t => t.FrontText != null && t.FrontText != "")
            .Select(t => t.FrontText!)
            .Distinct()
            .OrderBy(text => text)
            .ToListAsync();
    }
}
