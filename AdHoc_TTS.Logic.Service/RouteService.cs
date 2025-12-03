using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Services.Interfaces;

namespace AdHoc_SpeechSynthesizer.Services;

public class RouteService : IRouteService
{
    private readonly IRouteDao _dao;

    public RouteService(IRouteDao dao)
    {
        _dao = dao;
    }

    public Task<IEnumerable<Domain.Route>> GetAllAsync()
        => _dao.FindAllAsync();

    public Task<IEnumerable<int>> GetAllRouteNumbersAsync()
        => _dao.FindAllRouteNumbersAsync();

    Task<IEnumerable<Domain.Route>> IRouteService.GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
