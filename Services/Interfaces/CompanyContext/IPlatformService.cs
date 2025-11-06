using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IPlatformService
{
    /// <summary>
    /// Returns platforms, optionally filtered by control center, location type and/or location number.
    /// </summary>
    Task<List<Platform>> GetAllAsync(
        string? controlCenterId = null,
        int? locationTypeNr = null,
        int? locationNr = null);

    /// <summary>
    /// Returns all platforms for a given location (station) key.
    /// </summary>
    Task<List<Platform>> GetByLocationAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr);

    /// <summary>
    /// Returns a single platform by its composite key.
    /// </summary>
    Task<Platform?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr,
        int platformNr);
}
