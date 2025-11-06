using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IRouteService
{
    /// <summary>
    /// Returns routes, optionally filtered by control center, route number and/or variant.
    /// </summary>
    Task<List<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route>> GetAllAsync(
        string? controlCenterId = null,
        int? routeNr = null,
        string? routeVariant = null);

    /// <summary>
    /// Returns a single route by its composite key.
    /// </summary>
    Task<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int routeNr,
        string routeVariant);
}
