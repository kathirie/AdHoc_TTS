using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado
{
    public class LocationDao(CompanyDbContext db) :ILocationDao
    {
        private readonly CompanyDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<IEnumerable<Location>> FindAllAsync()
        {
            return await _db.Locations
                .AsNoTracking()
                .OrderBy(l => l.ControlCenterId)
                .ThenBy(l => l.LocationTypeNr)
                .ThenBy(l => l.LocationNr)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> FindAllRefLocationNamesAsync()
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
