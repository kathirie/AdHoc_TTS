using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface ITargetTextService
{
    Task<IEnumerable<TargetText>> GetAllAsync();

    Task<IEnumerable<string>> GetAllFrontTextsAsync();
}
