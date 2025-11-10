namespace AdHoc_SpeechSynthesizer.Models.AppContext;

public class TtsVoice
{
    public Guid VoiceId { get; set; }

    // FK to the model that owns this voice.
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
    public bool IsActive { get; set; } = true;
    public bool IsInstalled { get; set; } = false;

    // Navigation property to the owning model.
    public TtsModel Model { get; set; } = default!;
}
