using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces;


public interface ILocationService
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<IEnumerable<string>> GetAllRefLocationNamesAsync();
}

