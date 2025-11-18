using System.Speech.Synthesis;
using System.Text.Json;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public static class TtsVoiceSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await EnsureModelsAsync(db);

        var azureKey = config["SPEECH_KEY"];
        var azureRegion = config["SPEECH_REGION"];

        if (!string.IsNullOrWhiteSpace(azureKey) && !string.IsNullOrWhiteSpace(azureRegion))
        {
            await SeedAzureVoicesAsync(db, azureKey, azureRegion);
        }

        await SeedSapiVoicesAsync(db);
    }

    private static async Task EnsureModelsAsync(AppDbContext db)
    {
        if (!await db.TtsModels.AnyAsync(m => m.Provider == "azure"))
        {
            db.TtsModels.Add(new TtsModel
            {
                ModelId = Guid.NewGuid(),
                Provider = "azure",
                Name = "Azure Neural TTS"
            });
        }

        if (!await db.TtsModels.AnyAsync(m => m.Provider == "system.speech"))
        {
            db.TtsModels.Add(new TtsModel
            {
                ModelId = Guid.NewGuid(),
                Provider = "system.speech",
                Name = "System.Speech"
            });
        }

        await db.SaveChangesAsync();
    }

    // ------------------ Azure Voices ------------------
    private static async Task SeedAzureVoicesAsync(AppDbContext db, string key, string region)
    {
        using var http = new HttpClient();
        http.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

        var url = $"https://{region}.tts.speech.microsoft.com/cognitiveservices/voices/list";

        var allVoices = await http.GetFromJsonAsync<List<AzureVoiceResponse>>(url);
        if (allVoices is null) return;

        // only german voices
        var germanVoices = allVoices
            .Where(v => !string.IsNullOrWhiteSpace(v.Locale) &&
                        (v.Locale.Equals("de", StringComparison.OrdinalIgnoreCase) ||
                         v.Locale.StartsWith("de-", StringComparison.OrdinalIgnoreCase)))
            .ToList();

        var azureModel = await db.TtsModels
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Provider == "azure");

        if (azureModel is null)
        {
            Console.WriteLine("No 'azure' TTS model found. Run EnsureModelsAsync first.");
            return;
        }

        foreach (var v in germanVoices)
        {
            var exists = await db.TtsVoices.AnyAsync(x =>
                x.Provider == "azure" && x.ProviderVoiceId == v.ShortName);

            if (exists)
                continue;

            var stylesJson = JsonSerializer.Serialize(v.StyleList ?? new List<string>());

            db.TtsVoices.Add(new TtsVoice
            {
                VoiceId = Guid.NewGuid(),
                ModelId = azureModel.ModelId,
                Provider = "azure",
                ProviderVoiceId = v.ShortName,
                DisplayName = v.DisplayName,
                Locale = v.Locale,
                Gender = v.Gender,
                VoiceType = v.VoiceType,
                SampleRateHertz = int.TryParse(v.SampleRateHertz, out var sr) ? sr : (int?)null,
                StylesJson = stylesJson,
                Status = v.Status,
                IsInstalled = false
            });
        }

        await db.SaveChangesAsync();
        Console.WriteLine($"Azure: {germanVoices.Count} voices processed (new inserts via EF).");
    }

    // ------------------ System.Speech Voices ------------------
    private static async Task SeedSapiVoicesAsync(AppDbContext db)
    {
        using var synth = new SpeechSynthesizer();
        var installed = synth.GetInstalledVoices();

        var sapiModel = await db.TtsModels
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Provider == "system.speech");

        if (sapiModel is null)
        {
            Console.WriteLine("No 'system.speech' TTS model found.");
            return;
        }

        foreach (var v in installed)
        {
            var info = v.VoiceInfo;

            var exists = await db.TtsVoices.AnyAsync(x =>
                x.Provider == "system.speech" && x.ProviderVoiceId == info.Name);

            if (exists)
                continue;

            db.TtsVoices.Add(new TtsVoice
            {
                VoiceId = Guid.NewGuid(),
                ModelId = sapiModel.ModelId,
                Provider = "system.speech",
                ProviderVoiceId = info.Name,
                DisplayName = info.Name,
                Locale = info.Culture.Name,
                Gender = info.Gender.ToString(),
                VoiceType = "SAPI",
                Status = "Installed",
                IsInstalled = true
            });
        }

        await db.SaveChangesAsync();
        Console.WriteLine($"SAPI: {installed.Count} voices processed (new inserts via EF).");
    }

    // Helper-Record für Azure-API
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
