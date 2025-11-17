using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface ITargetTextService
{
    Task<IEnumerable<string>> GetAllFrontTextsAsync();

    Task<TargetText?> GetByKeyAsync(string controlCenterId, int versionNr, int targetTextNr);
}
