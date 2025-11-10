
namespace AdHoc_SpeechSynthesizer.Models.CompanyContext;

public class Platform
{
    public int VersionNr { get; set; }
    public int LocationTypeNr { get; set; }
    public int LocationNr { get; set; }
    public int PlatformNr { get; set; }
    public string ControlCenterId { get; set; } = null!;

    public string? Name { get; set; }
}
