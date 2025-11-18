using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface IPlatformService
{
    Task<IEnumerable<Platform>> GetAllAsync();
    Task<IEnumerable<int>> GetAllPlatformNumbersAsync();
}
