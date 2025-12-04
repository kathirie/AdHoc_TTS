using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Contracts.TtsVoices;

namespace AdHoc_SpeechSynthesizer.Contracts.TtsModels;

public static class TtsModelMapper
{
    public static TtsModelDto ToDto(this TtsModel model) =>
        new(
            model.ModelId,
            model.Provider,
            model.Name,
            model.Voices.Select(v => v.ToDto())
        );

    public static TtsModel ToDomain(this TtsModelDto dto) =>
        new()
        {
            ModelId = dto.ModelId,
            Provider = dto.Provider,
            Name = dto.Name,
            Voices = dto.Voices.Select(v => v.ToDomain()).ToList()
        };

    public static IEnumerable<TtsModelDto> ToDto(this IEnumerable<TtsModel> models) =>
        models.Select(m => m.ToDto());

    public static IEnumerable<TtsModel> ToDomain(this IEnumerable<TtsModelDto> dtos) =>
        dtos.Select(d => d.ToDomain());
}
