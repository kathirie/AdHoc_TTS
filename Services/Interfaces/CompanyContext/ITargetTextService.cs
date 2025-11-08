using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface ITargetTextService
{
    // Returns all FrontTexts.
    Task<List<string>> GetAllFrontTextsAsync();

    // Returns all target texts, optionally filtered by control center.
    Task<List<TargetText>> GetAllAsync(string? controlCenterId = null);

    // Returns a specific target text by composite key.
    Task<TargetText?> GetByKeyAsync(string controlCenterId, int versionNr, int targetTextNr);
}
