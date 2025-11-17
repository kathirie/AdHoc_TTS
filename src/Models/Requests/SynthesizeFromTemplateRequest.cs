namespace AdHoc_SpeechSynthesizer.Models.Requests;

public class SynthesizeFromTemplateRequest
{
    public Guid TemplateId { get; set; }

    public Guid ModelId { get; set; }
    public Guid? VoiceId { get; set; }

    public IEnumerable<string?>? RefLocationNames { get; set; }  // Location.RefLocationName
    public IEnumerable<string?>? PlatformNames { get; set; }     // Platform.Name
    public IEnumerable<int?>? RouteNrs { get; set; }             // Route.RouteNr
    public IEnumerable<string?>? FrontTexts { get; set; }        // TargetText.FrontText
    public IEnumerable<string?>? FreeTexts { get; set; }         // {custom text}
}
