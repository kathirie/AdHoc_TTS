using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IRouteService
{
    // Returns all RouteNrs.
    Task<List<int>> GetAllRouteNumbersAsync();

    // Returns routes, optionally filtered by control center, route number and/or variant.
    Task<List<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route>> GetAllAsync(
        string? controlCenterId = null,
        int? routeNr = null,
        string? routeVariant = null);

    // Returns a single route by its composite key.
    Task<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int routeNr,
        string routeVariant);
}
