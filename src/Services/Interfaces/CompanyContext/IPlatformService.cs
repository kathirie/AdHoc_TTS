using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IPlatformService
{
    Task<IEnumerable<int>> GetAllPlatformNumbersAsync();

    // Returns platforms, optionally filtered by control center, location type and/or location number.
    Task<IEnumerable<Platform>> GetAllAsync(
        string? controlCenterId = null,
        int? locationTypeNr = null,
        int? locationNr = null);

    // Returns all platforms for a given location key.
    Task<IEnumerable<Platform>> GetByLocationAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr);

    // Returns a platform for a given composite key.
    Task<Platform?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr,
        int platformNr);
}
