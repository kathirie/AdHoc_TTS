using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Dal.Interface
{
    public interface ILocationDao
    {
        Task<IEnumerable<Location>> FindAllAsync();
        Task<IEnumerable<string>> FindAllRefLocationNamesAsync();
    }
}
