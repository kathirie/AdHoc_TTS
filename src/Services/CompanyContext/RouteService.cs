using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.CompanyContext;

public class RouteService : IRouteService
{
    private readonly CompanyDbContext _db;

    public RouteService(CompanyDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route>> GetAllAsync()
    {
        var query = _db.Routes.AsNoTracking().AsQueryable();

        return await query
            .OrderBy(r => r.ControlCenterId)
            .ThenBy(r => r.RouteNr)
            .ThenBy(r => r.RouteVariant)
            .ToListAsync();
    }

    public async Task<IEnumerable<int>> GetAllRouteNumbersAsync()
    {
        return await _db.Routes
            .AsNoTracking()
            .Select(r => r.RouteNr)
            .Distinct()
            .OrderBy(nr => nr)
            .ToListAsync();
    }
}
