using AdHoc_SpeechSynthesizer.Api.Dtos;
using AdHoc_SpeechSynthesizer.Domain;


namespace AdHoc_SpeechSynthesizer.Api.Mapper;

public static class SynthesisRequestMapper
{
    public static SynthesisRequest ToDomain(this SynthesisRequestDto dto)
       => new()
       {
           ModelId = dto.ModelId,
           SsmlContent = dto.SsmlContent
       };

    public static SynthesisFromTemplateRequest ToDomain(this SynthesisFromTemplateRequestDto dto)
        => new()
        {
            TemplateId = dto.TemplateId,
            ModelId = dto.ModelId,
            RefLocationNames = dto.RefLocationNames,
            PlatformNumbers = dto.PlatformNumbers,
            RouteNrs = dto.RouteNrs,
            FrontTexts = dto.FrontTexts,
            FreeTexts = dto.FreeTexts
        };
}
