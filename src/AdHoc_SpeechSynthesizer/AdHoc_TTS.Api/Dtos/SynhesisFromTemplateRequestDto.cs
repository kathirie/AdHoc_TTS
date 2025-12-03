using System.ComponentModel.DataAnnotations;

namespace AdHoc_SpeechSynthesizer.Api.Dtos;

public class SynthesisFromTemplateRequestDto
{
    [Required]
    public Guid TemplateId { get; set; }

    [Required]
    public Guid ModelId { get; set; }

    public IEnumerable<string?>? RefLocationNames { get; set; }

    public IEnumerable<int?>? PlatformNumbers { get; set; }

    public IEnumerable<int?>? RouteNrs { get; set; }

    public IEnumerable<string?>? FrontTexts { get; set; }

    public IEnumerable<string?>? FreeTexts { get; set; }
}
