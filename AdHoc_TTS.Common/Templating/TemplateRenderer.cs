using System.Security;
using System.Text.RegularExpressions;

namespace AdHoc_SpeechSynthesizer.Common.Templating;

public static class TemplateRenderer
{
    private static readonly Regex PlaceholderRegex =
        new(@"\{(?<name>[^}]+)\}", RegexOptions.Compiled);

    public static string Render(string ssmlTemplate, IDictionary<string, IEnumerable<string>> values)
    {
        if (string.IsNullOrWhiteSpace(ssmlTemplate))
            throw new ArgumentException("SSML template is empty.", nameof(ssmlTemplate));

        var result = ssmlTemplate;

        if (values != null)
        {
            foreach (var kv in values)
            {
                var key = kv.Key;
                var placeholder = "{" + key + "}";

                var list = (kv.Value ?? Array.Empty<string>()).ToList();
                if (list.Count == 0)
                    continue;

                int occurrenceIndex = 0;
                int searchPos = 0;

                while (true)
                {
                    int pos = result.IndexOf(placeholder, searchPos, StringComparison.Ordinal);
                    if (pos < 0)
                        break;

                    if (occurrenceIndex >= list.Count)
                    {
                        throw new InvalidOperationException(
                            $"Not enough values provided for placeholder '{key}'. " +
                            $"Found {occurrenceIndex + 1} occurrences, but only {list.Count} values.");
                    }

                    var rawValue = list[occurrenceIndex] ?? string.Empty;

                    var escapedValue = SecurityElement.Escape(rawValue) ?? string.Empty;

                    result = result[..pos] + escapedValue + result[(pos + placeholder.Length)..];

                    // update search position
                    searchPos = pos + escapedValue.Length;
                    occurrenceIndex++;
                }
            }
        }

        // nach dem Ersetzen prüfen, ob noch {irgendwas} übrig ist
        var unmatched = PlaceholderRegex.Matches(result)
            .Select(m => m.Groups["name"].Value)
            .Distinct()
            .ToList();

        if (unmatched.Count > 0)
        {
            throw new InvalidOperationException(
                "No value provided for placeholder(s): " +
                string.Join(", ", unmatched));
        }

        return result;
    }
}
