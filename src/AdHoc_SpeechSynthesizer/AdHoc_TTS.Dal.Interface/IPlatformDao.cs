using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Dal.Interface;

public interface IPlatformDao
{
    Task<IEnumerable<Platform>> FindAllAsync();
    Task<IEnumerable<int>> FindAllPlatformNumbersAsync();
}
