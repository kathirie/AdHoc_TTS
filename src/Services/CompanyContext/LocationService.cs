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

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var query = _db.Locations.AsNoTracking().AsQueryable();

            return await query
                .OrderBy(l => l.ControlCenterId)
                .ThenBy(l => l.LocationTypeNr)
                .ThenBy(l => l.LocationNr)
                .ToListAsync();
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
    }
}
