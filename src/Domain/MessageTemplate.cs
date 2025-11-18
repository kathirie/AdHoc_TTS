namespace AdHoc_SpeechSynthesizer.Domain;

public class MessageTemplate
{
    public Guid TemplateId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string SsmlContent { get; set; } = default!;
}
