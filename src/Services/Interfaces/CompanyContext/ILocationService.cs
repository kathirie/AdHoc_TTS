using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;


public interface ILocationService
{
    Task<List<string>> GetAllRefLocationNamesAsync();

    // can be filtered by ControlCenterd and LocationTypeNr.
    Task<List<Location>> GetAllAsync(string? controlCenterId = null, int? locationTypeNr = null);

    Task<Location?> GetByKeyAsync(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr);
}

