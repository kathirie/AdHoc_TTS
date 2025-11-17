
namespace AdHoc_SpeechSynthesizer.Models.CompanyContext;

public class Route
{
    public int VersionNr { get; set; }
    public int RouteNr { get; set; }         
    public string RouteVariant { get; set; } = null!;
    public string ControlCenterId { get; set; } = null!;
}
