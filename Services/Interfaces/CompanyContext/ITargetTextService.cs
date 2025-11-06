using AdHoc_SpeechSynthesizer.Models.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

public interface ITargetTextService
{
    /// <summary>
    /// Returns all target texts, optionally filtered by control center.
    /// </summary>
    Task<List<TargetText>> GetAllAsync(string? controlCenterId = null);

    /// <summary>
    /// Returns a specific target text by composite key.
    /// </summary>
    Task<TargetText?> GetByKeyAsync(string controlCenterId, int versionNr, int targetTextNr);
}
