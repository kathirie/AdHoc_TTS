using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IRouteService
{
    Task<IEnumerable<AdHoc_SpeechSynthesizer.Models.CompanyContext.Route>> GetAllAsync();
    Task<IEnumerable<int>> GetAllRouteNumbersAsync();
}
