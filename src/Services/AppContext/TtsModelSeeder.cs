using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public static class TtsModelSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Azure Model
        if (!await db.TtsModels.AnyAsync(m => m.Provider == "azure"))
        {
            db.TtsModels.Add(new TtsModel
            {
                ModelId = Guid.NewGuid(),
                Provider = "azure",
                Name = "Azure Neural TTS"
            });
        }

        // System.Speech Model
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
        Console.WriteLine("EfTtsModelSeeder: Default models ensured via EF.");
    }
}
