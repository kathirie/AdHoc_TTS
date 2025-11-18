using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public static class MessageTemplateSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        const string baustelleName = "Baustelle";

        if (!await db.MessageTemplates.AnyAsync(t => t.Name == baustelleName))
        {
            var tmpl = new MessageTemplate
            {
                TemplateId = Guid.NewGuid(),
                Name = baustelleName,
                Description =
                    "Aufgrund von Bauarbeiten kann die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} nicht angefahren werden. " +
                    "Die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} wird daher ausgelassen.",
                SsmlContent =
@"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""de-DE"">
  <voice name=""de-DE-FlorianMultilingualNeural"">
    Aufgrund von Bauarbeiten kann die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} nicht angefahren werden.
    <break />
    Die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} wird daher ausgelassen.
  </voice>
</speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        const string stoerungName = "Technische Störungen";

        if (!await db.MessageTemplates.AnyAsync(t => t.Name == stoerungName))
        {
            var tmpl = new MessageTemplate
            {
                TemplateId = Guid.NewGuid(),
                Name = stoerungName,
                Description =
                    "Aufgrund von {Freitext} kommt es bei {Route.RouteNr} zu Verspätungen.",
                SsmlContent =
@"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""de-DE"">
  <voice name=""de-DE-FlorianMultilingualNeural"">
    Aufgrund von {Freitext} kommt es bei {Route.RouteNr} zu Verspätungen.
  </voice>
</speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        await db.SaveChangesAsync();
        Console.WriteLine("EfMessageTemplateSeeder: Templates ensured via EF.");
    }
}
