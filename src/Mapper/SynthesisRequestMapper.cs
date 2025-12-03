using AdHoc_TTS.Api.Dtos.Requests;
using AdHoc_TTS.Domain;

namespace AdHoc_TTS.Api.Mapper;

public static class SynthesisRequestMapper
{
    public static SynthesisRequest ToDomain(this SynthesisRequestDto dto)
       => new()
       {
           ModelId = dto.ModelId,
           VoiceId = dto.VoiceId,
           SsmlContent = dto.SsmlContent
       };

    public static SynthesisFromTemplateRequest ToDomain(this SynhesisFromTemplateRequestDto dto)
        => new()
        {
            TemplateId = dto.TemplateId,
            ModelId = dto.ModelId,
            VoiceId = dto.VoiceId,
            RefLocationNames = dto.RefLocationNames,
            PlatformNumbers = dto.PlatformNumbers,
            RouteNrs = dto.RouteNrs,
            FrontTexts = dto.FrontTexts,
            FreeTexts = dto.FreeTexts
        };
}
