using AdHoc_TTS.Api.Dtos.Responses;
using AdHoc_TTS.Domain;

namespace AdHoc_TTS.Api.Mapper;

public static class TemplateMapper
{
    public static TemplatePlaceholderResponseDto ToDto(this TemplatePlaceholder placeholder)
        => new() { Name = placeholder.Name };

    public static IEnumerable<TemplatePlaceholderResponseDto> ToDto(
        this IEnumerable<TemplatePlaceholder> placeholders)
        => placeholders.Select(p => p.ToDto());
}
