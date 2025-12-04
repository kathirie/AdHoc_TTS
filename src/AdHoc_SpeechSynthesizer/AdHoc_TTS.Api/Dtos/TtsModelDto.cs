using AdHoc_TTS.Api.Dtos;

namespace AdHoc_SpeechSynthesizer.Contracts.TtsModels;

public record TtsModelDto(
    Guid ModelId,
    string Provider,
    string Name,
    IEnumerable<TtsVoiceDto> Voices
);
