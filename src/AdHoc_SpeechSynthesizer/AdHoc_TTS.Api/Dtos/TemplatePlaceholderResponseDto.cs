using System.ComponentModel.DataAnnotations;

namespace AdHoc_SpeechSynthesizer.Api.Dtos;

public class TemplatePlaceholderResponseDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
