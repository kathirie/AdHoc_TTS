using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using AdHoc_SpeechSynthesizer.Models.CompanyContext;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.CompanyContext;

public class RouteService : IRouteService
{
    private readonly CompanyDbContext _db;

    public RouteService(CompanyDbContext db)
    {
        _db = db;
    }

    public async Task<List<int>> GetAllRouteNumbersAsync()
    {
        return await _db.Routes
            .AsNoTracking()
            .Select(r => r.RouteNr)
            .Distinct()
            .OrderBy(nr => nr)
            .ToListAsync();
    }

    public async Task<List<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route>> GetAllAsync(
        string? controlCenterId = null,
        int? routeNr = null,
        string? routeVariant = null)
    {
        var query = _db.Routes.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(controlCenterId))
            query = query.Where(r => r.ControlCenterId == controlCenterId);

        if (routeNr.HasValue)
            query = query.Where(r => r.RouteNr == routeNr.Value);

        if (!string.IsNullOrWhiteSpace(routeVariant))
            query = query.Where(r => r.RouteVariant == routeVariant);

        return await query
            .OrderBy(r => r.ControlCenterId)
            .ThenBy(r => r.RouteNr)
            .ThenBy(r => r.RouteVariant)
            .ToListAsync();
    }

    public async Task<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int routeNr,
        string routeVariant)
    {
        return await _db.Routes
            .AsNoTracking()
            .FirstOrDefaultAsync(r =>
                r.ControlCenterId == controlCenterId &&
                r.VersionNr == versionNr &&
                r.RouteNr == routeNr &&
                r.RouteVariant == routeVariant);
    }
}
