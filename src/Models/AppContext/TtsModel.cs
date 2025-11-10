namespace AdHoc_SpeechSynthesizer.Models.AppContext;

public class TtsModel
{
    public Guid ModelId { get; set; }
    public string Provider { get; set; } = default!;
    public string Name { get; set; } = default!;

    // configuration or metadata (e.g. API region, endpoint, etc.).
    public string? SettingsJson { get; set; }
    public bool IsActive { get; set; } = true;


    // Navigation property for related voices.
    public ICollection<TtsVoice> Voices { get; set; } = new List<TtsVoice>();
}
