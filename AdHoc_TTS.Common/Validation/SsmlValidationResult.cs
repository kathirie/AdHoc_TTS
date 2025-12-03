namespace AdHoc_SpeechSynthesizer.Common.Validation
{
    public record SsmlValidationResult(
        bool IsXmlWellFormed,
        bool IsSchemaValid,
        IReadOnlyList<string> Errors);
}
