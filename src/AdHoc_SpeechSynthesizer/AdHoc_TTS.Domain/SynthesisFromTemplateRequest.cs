namespace AdHoc_SpeechSynthesizer.Domain;

public class SynthesisFromTemplateRequest
{
    public Guid TemplateId { get; set; }

    public Guid ModelId { get; set; }
    public Guid? VoiceId { get; set; }

    public IEnumerable<string?>? RefLocationNames { get; set; }
    public IEnumerable<int?>? PlatformNumbers { get; set; }
    public IEnumerable<int?>? RouteNrs { get; set; }
    public IEnumerable<string?>? FrontTexts { get; set; }
    public IEnumerable<string?>? FreeTexts { get; set; }
}