namespace AdHoc_SpeechSynthesizer.Services.Interfaces;

public interface IRouteService
{
    Task<IEnumerable<Domain.Route>> GetAllAsync();
    Task<IEnumerable<int>> GetAllRouteNumbersAsync();
}
