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

        public async Task<List<string>> GetAllPlatformNamesAsync()
        {
            return await _db.Platforms
                .AsNoTracking()
                .Where(p => p.Name != null && p.Name != "")
                .Select(p => p.Name!)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();
        }

        public async Task<List<Platform>> GetAllAsync(
            string? controlCenterId = null,
            int? locationTypeNr = null,
            int? locationNr = null)
        {
            var query = _db.Platforms.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(controlCenterId))
                query = query.Where(p => p.ControlCenterId == controlCenterId);

            if (locationTypeNr.HasValue)
                query = query.Where(p => p.LocationTypeNr == locationTypeNr.Value);

            if (locationNr.HasValue)
                query = query.Where(p => p.LocationNr == locationNr.Value);

            return await query
                .OrderBy(p => p.ControlCenterId)
                .ThenBy(p => p.LocationTypeNr)
                .ThenBy(p => p.LocationNr)
                .ThenBy(p => p.PlatformNr)
                .ToListAsync();
        }

        public async Task<List<Platform>> GetByLocationAsync(
            string controlCenterId,
            int versionNr,
            int locationTypeNr,
            int locationNr)
        {
            return await _db.Platforms
                .AsNoTracking()
                .Where(p =>
                    p.ControlCenterId == controlCenterId &&
                    p.VersionNr == versionNr &&
                    p.LocationTypeNr == locationTypeNr &&
                    p.LocationNr == locationNr)
                .OrderBy(p => p.PlatformNr)
                .ToListAsync();
        }

        public async Task<Platform?> GetByKeyAsync(
            string controlCenterId,
            int versionNr,
            int locationTypeNr,
            int locationNr,
            int platformNr)
        {
            return await _db.Platforms
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.ControlCenterId == controlCenterId &&
                    p.VersionNr == versionNr &&
                    p.LocationTypeNr == locationTypeNr &&
                    p.LocationNr == locationNr &&
                    p.PlatformNr == platformNr);
        }
    }
}
