namespace AdHoc_SpeechSynthesizer.Dal.Interface;

public interface IRouteDao
{
    Task<IEnumerable<AdHoc_SpeechSynthesizer.Domain.Route>> FindAllAsync();
    Task<IEnumerable<int>> FindAllRouteNumbersAsync();
}
