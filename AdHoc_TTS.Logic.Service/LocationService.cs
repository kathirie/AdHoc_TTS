

using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces;

namespace AdHoc_SpeechSynthesizer.Services;

public class LocationService : ILocationService
{
    private readonly ILocationDao _dao;

    public LocationService(ILocationDao dao)
    {
        _dao = dao;
    }

    public Task<IEnumerable<Location>> GetAllAsync()
        => _dao.FindAllAsync();

    public Task<IEnumerable<string>> GetAllRefLocationNamesAsync()
        => _dao.FindAllRefLocationNamesAsync();
}
