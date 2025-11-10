using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IPlatformService
{
    Task<List<string>> GetAllPlatformNamesAsync();

    // Returns platforms, optionally filtered by control center, location type and/or location number.
    Task<List<Platform>> GetAllAsync(
        string? controlCenterId = null,
        int? locationTypeNr = null,
        int? locationNr = null);


    // Returns all platforms for a given location key.
    Task<List<Platform>> GetByLocationAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr);

    Task<Platform?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr,
        int platformNr);
}
