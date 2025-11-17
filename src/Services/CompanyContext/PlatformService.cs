using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.CompanyContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.CompanyContext
{
    public class PlatformService : IPlatformService
    {
        private readonly CompanyDbContext _db;

        public PlatformService(CompanyDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            var query = _db.Platforms.AsNoTracking().AsQueryable();

            return await query
                .OrderBy(p => p.ControlCenterId)
                .ThenBy(p => p.LocationTypeNr)
                .ThenBy(p => p.LocationNr)
                .ThenBy(p => p.PlatformNr)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllPlatformNumbersAsync()
        {
            return await _db.Platforms
                .AsNoTracking()
                .Where(p => p.PlatformNr >= 0)
                .Select(p => p.PlatformNr!)
                .Distinct()
                .OrderBy(platformnumber => platformnumber)
                .ToListAsync();
        }
    }
}
