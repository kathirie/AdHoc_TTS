namespace AdHoc_TTS.Api.Dtos;

public  record TtsVoiceDto(
    Guid VoiceId,
    Guid ModelId,
    string Provider,
    string ProviderVoiceId,
    string DisplayName,
    string Locale,
    string? Gender,
    string? VoiceType,
    int? SampleRateHertz,
    string? StylesJson,
    string? Status,
    bool IsInstalled
);
