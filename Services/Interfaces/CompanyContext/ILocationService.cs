using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;


public interface ILocationService
{
    // Returns all RefLocationNames.
    Task<List<string>> GetAllRefLocationNamesAsync();

    // Returns all Locations, can be filtered by ControlCenterd and LocationTypeNr.
    Task<List<Location>> GetAllAsync(string? controlCenterId = null, int? locationTypeNr = null);

    // Returns a specific Location by its composite key.
    Task<Location?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr);
}

