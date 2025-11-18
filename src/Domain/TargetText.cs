namespace AdHoc_SpeechSynthesizer.Domain;

public class TargetText
{
    public int VersionNr { get; set; }
    public int TargetTextNr { get; set; }
    public string ControlCenterId { get; set; } = null!;
    public string? FrontText { get; set; }
}
