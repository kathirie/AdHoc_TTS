using System.Xml;
using System.Xml.Linq;

namespace AdHoc_SpeechSynthesizer.Common.Validation;

public class SsmlValidator
{
    public SsmlValidationResult Validate(string ssml)
    {
        var errors = new List<string>();
        bool xmlOk = false;
        bool semanticOk = false;

        if (string.IsNullOrWhiteSpace(ssml))
        {
            errors.Add("SSML is empty.");
            return new SsmlValidationResult(false, false, errors);
        }

        // check for forgotten placeholders
        if (ssml.Contains("{") || ssml.Contains("}"))
        {
            errors.Add("SSML still contains '{' or '}'. Possible unresolved placeholders.");
        }

        XDocument doc;

        // Well-Formedness-Validation
        try
        {
            // XmlReader
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true
            };

            using (var reader = XmlReader.Create(new StringReader(ssml), settings))
            {
                while (reader.Read())
                {
                    
                }
            }

            doc = XDocument.Parse(ssml, LoadOptions.SetLineInfo);
            xmlOk = true;
        }
        catch (XmlException ex)
        {
            errors.Add($"Not well-formed XML: {ex.Message}");
            return new SsmlValidationResult(xmlOk, semanticOk, errors);
        }

        // check for Root <speak>
        var root = doc.Root;
        if (root is null || root.Name.LocalName != "speak")
        {
            errors.Add("Root element must be <speak>.");
        }

        // 4) check for <voice>-Element + name-Attribute
        var voiceElements = root?
            .Descendants()
            .Where(e => e.Name.LocalName == "voice")
            .ToList() ?? new List<XElement>();

        if (voiceElements.Count == 0)
        {
            errors.Add("SSML must contain at least one <voice> element.");
        }
        else
        {
            foreach (var voice in voiceElements)
            {
                var nameAttr = voice.Attribute("name");
                if (nameAttr == null || string.IsNullOrWhiteSpace(nameAttr.Value))
                {
                    errors.Add("Each <voice> element must have a non-empty 'name' attribute.");
                }
            }
        }

        semanticOk = xmlOk
                     && voiceElements.Count > 0
                     && !errors.Any(e =>
                        e.StartsWith("Root element must be", StringComparison.OrdinalIgnoreCase) ||
                        e.StartsWith("Each <voice> element", StringComparison.OrdinalIgnoreCase) ||
                        e.StartsWith("SSML must contain at least one <voice>", StringComparison.OrdinalIgnoreCase) ||
                        e.StartsWith("Not well-formed XML", StringComparison.OrdinalIgnoreCase));

        return new SsmlValidationResult(xmlOk, semanticOk, errors);
    }
}
