using System.ComponentModel.DataAnnotations;

namespace AdHoc_SpeechSynthesizer.Api.Dtos;

public record SynthesisRequestDto
{
    [Required]
    public Guid ModelId { get; set; }

    [Required]
    public string SsmlContent { get; set; } = string.Empty;
}
