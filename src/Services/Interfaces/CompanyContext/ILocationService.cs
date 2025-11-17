using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;


public interface ILocationService
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<IEnumerable<string>> GetAllRefLocationNamesAsync();
}

