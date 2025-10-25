namespace AdHoc_SpeechSynthesizer.Models
{
    /// <summary>
    /// Represents a specific voice within a TTS model (e.g. Azure Jenny, SAPI Hedda).
    /// </summary>
    public class TtsVoice
    {
        public Guid VoiceId { get; set; }

        /// <summary>
        /// FK to the model that owns this voice.
        /// </summary>
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
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property to the owning model.
        /// </summary>
        public TtsModel Model { get; set; } = default!;
    }
}
