using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Contracts.MessageTemplates;

public static class MessageTemplateMapper
{
    public static MessageTemplateDto ToDto(this MessageTemplate template) =>
        new(
            template.TemplateId,
            template.Name,
            template.Description,
            template.SsmlContent
        );

    public static MessageTemplate ToDomain(this MessageTemplateDto dto) =>
        new()
        {
            TemplateId = dto.TemplateId,
            Name = dto.Name,
            Description = dto.Description,
            SsmlContent = dto.SsmlContent
        };

    public static IEnumerable<MessageTemplateDto> ToDto(this IEnumerable<MessageTemplate> templates) =>
        templates.Select(t => t.ToDto());

    public static IEnumerable<MessageTemplate> ToDomain(this IEnumerable<MessageTemplateDto> dtos) =>
        dtos.Select(d => d.ToDomain());
}
