using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado;

public class TargetTextDao(CompanyDbContext db) : ITargetTextDao
{
    private readonly CompanyDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

    public async Task<IEnumerable<TargetText>> FindAllAsync()
    {
        return await _db.TargetTexts
            .AsNoTracking()
            .OrderBy(t => t.ControlCenterId)
            .ThenBy(t => t.VersionNr)
            .ThenBy(t => t.TargetTextNr)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> FindAllFrontTextsAsync()
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
