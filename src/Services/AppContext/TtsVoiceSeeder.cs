using System.Speech.Synthesis;
using System.Text.Json;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public static class TtsVoiceSeeder
{
    // Entry point for seeding all voices (Azure + SAPI).
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("No connection string configured.");

        var azureKey = config["SPEECH_KEY"];
        var azureRegion = config["SPEECH_REGION"];

        if (!string.IsNullOrWhiteSpace(azureKey) && !string.IsNullOrWhiteSpace(azureRegion))
        {
            Console.WriteLine("Seeding Azure voices...");
            await SeedAzureVoicesAsync(connectionString, azureKey, azureRegion);
        }
        else
        {
            Console.WriteLine("Azure Speech key or region not set — skipping Azure voice seeding.");
        }

        Console.WriteLine("Seeding System.Speech (SAPI) voices...");
        await SeedSapiVoicesAsync(connectionString);

        Console.WriteLine("Voice seeding finished.");
    }

    private const string schema = "dbo";

    // ---------------------- AZURE ----------------------
    private static async Task SeedAzureVoicesAsync(string connectionString, string key, string region)
    {
        using var http = new HttpClient();
        http.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
        var url = $"https://{region}.tts.speech.microsoft.com/cognitiveservices/voices/list";

        var allVoices = await http.GetFromJsonAsync<List<AzureVoiceResponse>>(url);
        if (allVoices is null)
        {
            Console.WriteLine("Failed to fetch Azure voices.");
            return;
        }

        var germanVoices = allVoices.FindAll(v =>
            !string.IsNullOrWhiteSpace(v.Locale) &&
            (v.Locale.Equals("de", StringComparison.OrdinalIgnoreCase) ||
             v.Locale.StartsWith("de-", StringComparison.OrdinalIgnoreCase)));
        if (germanVoices is null)
        {
            Console.WriteLine("There are no german voices available.");
            return;
        }

        using var conn = new SqlConnection(connectionString);

        var modelId = await conn.ExecuteScalarAsync<Guid?>(
            $"SELECT ModelId FROM [{schema}].[TtsModel] WHERE Provider = @p",
            new { p = "azure" });

        if (modelId is null)
        {
            Console.WriteLine("No 'azure' TTS model found. Run TtsModelSeeder first.");
            return;
        }

        var insertSql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM [{schema}].[TtsVoice]
                    WHERE Provider = 'azure' AND ProviderVoiceId = @ProviderVoiceId
                )
                BEGIN
                    INSERT INTO [{schema}].[TtsVoice]
                    (VoiceId, ModelId, Provider, ProviderVoiceId, DisplayName, Locale, Gender, VoiceType, SampleRateHertz, StylesJson, Status, IsActive, IsInstalled, CreatedAt, UpdatedAt)
                    VALUES
                    (NEWID(), @ModelId, 'azure', @ProviderVoiceId, @DisplayName, @Locale, @Gender, @VoiceType, @SampleRateHertz, @StylesJson, @Status, 1, 0, SYSUTCDATETIME(), SYSUTCDATETIME())
                END";

        int count = 0;
        foreach (var v in germanVoices)
        {
            var stylesJson = JsonSerializer.Serialize(v.StyleList ?? new List<string>());
            count += await conn.ExecuteAsync(insertSql, new
            {
                ModelId = modelId.Value,
                ProviderVoiceId = v.ShortName,
                v.DisplayName,
                v.Locale,
                v.Gender,
                v.VoiceType,
                SampleRateHertz = int.TryParse(v.SampleRateHertz, out var sr) ? sr : (int?)null,
                StylesJson = stylesJson,
                v.Status
            });
        }

        Console.WriteLine($"Azure: {germanVoices.Count} voices processed (new inserts: {count}).");
    }

    // ---------------------- SAPI / SYSTEM.SPEECH ----------------------
    private static async Task SeedSapiVoicesAsync(string connectionString)
    {
        using var synth = new SpeechSynthesizer();
        var installed = synth.GetInstalledVoices();

        using var conn = new SqlConnection(connectionString);

        // Get the ModelId for System.Speech
        var modelId = await conn.ExecuteScalarAsync<Guid?>(
            $"SELECT ModelId FROM [{schema}].[TtsModel] WHERE Provider = @p",
            new { p = "system.speech" });

        if (modelId is null)
        {
            Console.WriteLine("No 'system.speech' TTS model found. Run TtsModelSeeder first.");
            return;
        }

        var insertSql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM [{schema}].[TtsVoice]
                    WHERE Provider = 'system.speech' AND ProviderVoiceId = @ProviderVoiceId
                )
                BEGIN
                    INSERT INTO [{schema}].[TtsVoice]
                      (VoiceId, ModelId, Provider, ProviderVoiceId, DisplayName, Locale, Gender, VoiceType, Status, IsActive, IsInstalled, CreatedAt, UpdatedAt)
                    VALUES
                      (NEWID(), @ModelId, 'system.speech', @ProviderVoiceId, @DisplayName, @Locale, @Gender, 'SAPI', 'Installed', 1, 1, SYSUTCDATETIME(), SYSUTCDATETIME())
                END";

        int count = 0;
        foreach (var v in installed)
        {
            var info = v.VoiceInfo;
            count += await conn.ExecuteAsync(insertSql, new
            {
                ModelId = modelId.Value,
                ProviderVoiceId = info.Name,
                DisplayName = info.Name,
                Locale = info.Culture.Name,
                Gender = info.Gender.ToString()
            });
        }

        Console.WriteLine($"SAPI: {installed.Count} voices processed (new inserts: {count}).");
    }

    // ---------------------- Helper ----------------------
    private record AzureVoiceResponse(
        string ShortName,
        string DisplayName,
        string LocalName,
        string Gender,
        string Locale,
        string VoiceType,
        string Status,
        string SampleRateHertz,
        List<string>? StyleList
    );
}
