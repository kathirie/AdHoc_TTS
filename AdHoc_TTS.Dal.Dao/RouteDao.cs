using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado;

public class RouteDao : IRouteDao
{
    private readonly CompanyDbContext _db;

    public RouteDao(CompanyDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Route>> FindAllAsync()
    {
        return await _db.Routes
            .AsNoTracking()
            .OrderBy(r => r.ControlCenterId)
            .ThenBy(r => r.RouteNr)
            .ThenBy(r => r.RouteVariant)
            .ToListAsync();
    }

    public async Task<IEnumerable<int>> FindAllRouteNumbersAsync()
    {
        return await _db.Routes
            .AsNoTracking()
            .Select(r => r.RouteNr) 
            .Distinct()
            .OrderBy(nr => nr)
            .ToListAsync();
    }
}
