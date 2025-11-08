namespace AdHoc_SpeechSynthesizer.Models.Requests;

public class SynthesizeFromTemplateRequest
{
    // transform into lists for multiple inputs later
    public Guid TemplateId { get; set; }

    public Guid ModelId { get; set; }
    public Guid? VoiceId { get; set; }

    public string? RefLocationName { get; set; }  // Location.RefLocationName
    public string? PlatformName { get; set; }     // Platform.Name
    public int? RouteNr { get; set; }             // Route.RouteNr
    public string? FrontText { get; set; }        // TargetText.FrontText
    public string? FreeText { get; set; }         // {Freitext}
}
