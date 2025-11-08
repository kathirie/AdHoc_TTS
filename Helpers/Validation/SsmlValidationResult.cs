using System.Collections.Generic;

namespace AdHoc_SpeechSynthesizer.Helpers.Validation
{
    public record SsmlValidationResult(
        bool IsXmlWellFormed,
        bool IsSchemaValid,
        IReadOnlyList<string> Errors);
}
