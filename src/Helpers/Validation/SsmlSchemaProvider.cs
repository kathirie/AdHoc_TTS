using System.Reflection;
using System.Xml.Schema;

namespace AdHoc_SpeechSynthesizer.Helpers.Validation
{
    public static class SsmlSchemaProvider
    {
        private static readonly Lazy<XmlSchemaSet> _schemas = new(CreateSchemas);

        public static XmlSchemaSet Schemas => _schemas.Value;

        private static XmlSchemaSet CreateSchemas()
        {
            var assembly = Assembly.GetExecutingAssembly();

            const string resourceName = "AdHoc_SpeechSynthesizer.Schemas.synthesis-core.xsd";

            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException(
                    $"Embedded resource '{resourceName}' not found. " +
                    $"Check namespace/folder/Build Action.");

            var schemas = new XmlSchemaSet();

            var schema = XmlSchema.Read(stream, (sender, e) =>
            {
                throw new InvalidOperationException(
                    $"Error reading SSML schema: {e.Message}");
            });

            schemas.Add(schema);

            return schemas;
        }
    }
}
