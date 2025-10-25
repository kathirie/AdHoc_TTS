namespace AdHoc_SpeechSynthesizer.Models
{
    /// <summary>
    /// Represents a TTS model or provider engine (e.g. Azure Speech, System.Speech).
    /// Each model can have many voices.
    /// </summary>
    public class TtsModel
    {
        public Guid ModelId { get; set; }

        public string Provider { get; set; } = default!;

        public string Name { get; set; } = default!;

        /// <summary>
        /// configuration or metadata (e.g. API region, endpoint, etc.).
        /// </summary>
        public string? SettingsJson { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property for related voices.
        /// </summary>
        public ICollection<TtsVoice> Voices { get; set; } = new List<TtsVoice>();
    }
}
