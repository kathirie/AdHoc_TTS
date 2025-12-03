namespace AdHoc_SpeechSynthesizer.Domain;
public class SynthesisRequest
{
    public Guid ModelId { get; set; }
    public Guid? VoiceId { get; set; }
    public string SsmlContent { get; set; } = string.Empty;
}