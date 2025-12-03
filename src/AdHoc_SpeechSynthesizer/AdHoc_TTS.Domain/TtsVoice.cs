namespace AdHoc_SpeechSynthesizer.Domain;

public class TtsVoice
{
    public Guid VoiceId { get; set; }
    public Guid ModelId { get; set; }
    public string Provider { get; set; } = default!;
    public string ProviderVoiceId { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Locale { get; set; } = default!;
    public string? Gender { get; set; }
    public string? VoiceType { get; set; }
    public int? SampleRateHertz { get; set; }
    public string? StylesJson { get; set; }
    public string? Status { get; set; }
    public bool IsInstalled { get; set; } = false;

    // Navigation property to the owning model.
    public TtsModel Model { get; set; } = default!;
}
