namespace AdHoc_SpeechSynthesizer.Models.Requests;

public class SynthesisRequest
{
    public Guid ModelId { get; set; }
    public Guid? VoiceId { get; set; }
    public string SsmlContent { get; set; } = string.Empty;
}
