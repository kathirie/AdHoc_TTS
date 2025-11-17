using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.CompanyContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.CompanyContext
{
    public class LocationService : ILocationService
    {
        private readonly CompanyDbContext _db;

        public LocationService(CompanyDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<string>> GetAllRefLocationNamesAsync()
        {
            return await _db.Locations
                .AsNoTracking()
                .Where(l => l.RefLocationName != null && l.RefLocationName != "")
                .Select(l => l.RefLocationName!)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();
        }

        public async Task<List<Location>> GetAllAsync(string? controlCenterId = null, int? locationTypeNr = null)
        {
            var query = _db.Locations.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(controlCenterId))
                query = query.Where(l => l.ControlCenterId == controlCenterId);

            if (locationTypeNr.HasValue)
                query = query.Where(l => l.LocationTypeNr == locationTypeNr.Value);

            return await query
                .OrderBy(l => l.ControlCenterId)
                .ThenBy(l => l.LocationTypeNr)
                .ThenBy(l => l.LocationNr)
                .ToListAsync();
        }

        public async Task<Location?> GetByKeyAsync(
            string controlCenterId,
            int versionNr,
            int locationTypeNr,
            int locationNr)
        {
            return await _db.Locations
                .AsNoTracking()
                .FirstOrDefaultAsync(l =>
                    l.ControlCenterId == controlCenterId &&
                    l.VersionNr == versionNr &&
                    l.LocationTypeNr == locationTypeNr &&
                    l.LocationNr == locationNr);
        }
    }
}
