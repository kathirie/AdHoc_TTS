using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_TTS.Api.Dtos;

namespace AdHoc_SpeechSynthesizer.Contracts.TtsVoices;

public static class TtsVoiceMapper
{
    public static TtsVoiceDto ToDto(this TtsVoice voice) =>
        new(
            voice.VoiceId,
            voice.ModelId,
            voice.Provider,
            voice.ProviderVoiceId,
            voice.DisplayName,
            voice.Locale,
            voice.Gender,
            voice.VoiceType,
            voice.SampleRateHertz,
            voice.StylesJson,
            voice.Status,
            voice.IsInstalled
        );

    public static TtsVoice ToDomain(this TtsVoiceDto dto) =>
        new()
        {
            VoiceId = dto.VoiceId,
            ModelId = dto.ModelId,
            Provider = dto.Provider,
            ProviderVoiceId = dto.ProviderVoiceId,
            DisplayName = dto.DisplayName,
            Locale = dto.Locale,
            Gender = dto.Gender,
            VoiceType = dto.VoiceType,
            SampleRateHertz = dto.SampleRateHertz,
            StylesJson = dto.StylesJson,
            Status = dto.Status,
            IsInstalled = dto.IsInstalled
        };

    public static IEnumerable<TtsVoiceDto> ToDto(this IEnumerable<TtsVoice> voices) =>
        voices.Select(v => v.ToDto());

    public static IEnumerable<TtsVoice> ToDomain(this IEnumerable<TtsVoiceDto> dtos) =>
        dtos.Select(d => d.ToDomain());
}
