namespace AdHoc_SpeechSynthesizer.Models
{
    public class MessageTemplate
    {
        public Guid TemplateId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; } = default!;

        public string SSMLContent { get; set; } = default!;
    }
}
