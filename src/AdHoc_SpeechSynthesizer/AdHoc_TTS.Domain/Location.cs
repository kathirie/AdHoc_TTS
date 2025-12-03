namespace AdHoc_SpeechSynthesizer.Domain;
    public class Location
    {
        public int VersionNr { get; set; }
        public int LocationTypeNr { get; set; }
        public int LocationNr { get; set; }
        public string ControlCenterId { get; set; } = null!;
        public string? RefLocationName { get; set; }
    }

