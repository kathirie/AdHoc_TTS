using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces;

namespace AdHoc_SpeechSynthesizer.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformDao _dao;

        public PlatformService(IPlatformDao dao)
        {
            _dao = dao;
        }

        public Task<IEnumerable<Platform>> GetAllAsync()
            => _dao.FindAllAsync();

        public Task<IEnumerable<int>> GetAllPlatformNumbersAsync()
            => _dao.FindAllPlatformNumbersAsync();
    }
}
