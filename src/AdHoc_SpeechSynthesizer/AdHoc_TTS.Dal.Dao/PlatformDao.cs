using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado;

public class PlatformDao(CompanyDbContext db) : IPlatformDao
{
    private readonly CompanyDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

    public async Task<IEnumerable<Platform>> FindAllAsync()
    {
        return await _db.Platforms
            .AsNoTracking()
            .OrderBy(p => p.ControlCenterId)
            .ThenBy(p => p.LocationTypeNr)
            .ThenBy(p => p.LocationNr)
            .ThenBy(p => p.PlatformNr)
            .ToListAsync();
    }

    public async Task<IEnumerable<int>> FindAllPlatformNumbersAsync()
    {
        return await _db.Platforms
            .AsNoTracking()
            .Where(p => p.PlatformNr >= 0)
            .Select(p => p.PlatformNr)  
            .Distinct()
            .OrderBy(nr => nr)
            .ToListAsync();
    }
}
