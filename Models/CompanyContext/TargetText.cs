using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

namespace AdHoc_SpeechSynthesizer.Models.CompanyContext;

public class TargetText
{
    public int VersionNr { get; set; }
    public int TargetTextNr { get; set; }
    public string ControlCenterId { get; set; } = null!;

    public string? FrontText { get; set; }

}
