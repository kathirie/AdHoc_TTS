namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IRouteService
{
    Task<IEnumerable<Domain.Route>> GetAllAsync();
    Task<IEnumerable<int>> GetAllRouteNumbersAsync();
}
