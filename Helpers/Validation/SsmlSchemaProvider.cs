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

            // Adjust to your actual namespace + file name
            const string resourceName = "AdHoc_SpeechSynthesizer.Schemas.synthesis-core.xsd";

            // Debug helper if needed:
            // var names = assembly.GetManifestResourceNames();

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

            // If you like, specify the namespace explicitly:
            // schemas.Add("http://www.w3.org/2001/10/synthesis", schema);
            schemas.Add(schema);

            return schemas;
        }
    }
}
