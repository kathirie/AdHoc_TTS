using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Services.Interfaces;

namespace AdHoc_SpeechSynthesizer.Services;

public class RouteService(IRouteDao dao) : IRouteService
{
    private readonly IRouteDao _dao = dao ?? throw new ArgumentNullException(nameof(dao));

    public Task<IEnumerable<Domain.Route>> GetAllAsync()
        => _dao.FindAllAsync();

    public Task<IEnumerable<int>> GetAllRouteNumbersAsync()
        => _dao.FindAllRouteNumbersAsync();

    Task<IEnumerable<Domain.Route>> IRouteService.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
