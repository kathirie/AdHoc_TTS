using System.Security;
using System.Text.RegularExpressions;

namespace AdHoc_SpeechSynthesizer.Helpers
{
    public static class TemplateRenderer
    {
        // placeholder recognition
        private static readonly Regex PlaceholderRegex =
            new(@"\{(?<name>[^}]+)\}", RegexOptions.Compiled);

        public static string Render(string ssmlTemplate, IDictionary<string, string> values)
        {
            if (string.IsNullOrWhiteSpace(ssmlTemplate))
                throw new ArgumentException("SSML template is empty.", nameof(ssmlTemplate));

            return PlaceholderRegex.Replace(ssmlTemplate, match =>
            {
                var name = match.Groups["name"].Value;

                if (!values.TryGetValue(name, out var rawValue) || string.IsNullOrWhiteSpace(rawValue))
                {
                    throw new InvalidOperationException(
                        $"No value provided for placeholder '{name}'.");
                }

                var escaped = SecurityElement.Escape(rawValue) ?? string.Empty;
                return escaped;
            });
        }
    }
}
