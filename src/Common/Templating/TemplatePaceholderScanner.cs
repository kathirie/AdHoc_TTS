namespace AdHoc_SpeechSynthesizer.Common.Templating;

using System.Text.RegularExpressions;


public static class TemplatePlaceholderScanner
{
    private static readonly Regex PlaceholderRegex =
        new(@"\{(?<name>[^}]+)\}", RegexOptions.Compiled);

    public static IEnumerable<string> GetPlaceholders(string ssmlTemplate)
    {
        if (string.IsNullOrWhiteSpace(ssmlTemplate))
            return Array.Empty<string>();

        var matches = PlaceholderRegex.Matches(ssmlTemplate);

        var names = matches
            .Select(m => m.Groups["name"].Value)
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Distinct(StringComparer.Ordinal)
            .ToList();

        return names;
    }
}


