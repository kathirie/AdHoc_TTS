namespace AdHoc_SpeechSynthesizer.Contracts.MessageTemplates;

public record MessageTemplateDto(
    Guid TemplateId,
    string Name,
    string? Description,
    string SsmlContent
);
