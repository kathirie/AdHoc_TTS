using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdHoc_SpeechSynthesizer.Services;

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
                    @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""de-AT"">
                      <voice name=""de-AT-IngridNeural"">
                        Aufgrund von Bauarbeiten kann die Haltestelle {Location.RefLocationName} {Platform.PlatformNr}<break time=""50ms""/> nicht angefahren werden.
                        <break />
                        Die Haltestelle {Location.RefLocationName} {Platform.PlatformNr}<break time=""50ms""/> wird daher ausgelassen.
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
                    "Aufgrund von {Freitext} kommt es bei der Linie {Route.RouteNr} zu Verspätungen.",
                SsmlContent =
                    @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""de-DE"">
                      <voice name=""de-AT-IngridNeural"">
                        Aufgrund von {Freitext} kommt es bei der Linie {Route.RouteNr}<break time=""50ms""/> zu Verspätungen.
                      </voice>
                    </speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        const string shortName = "Kurze Durchsage";

        if (!await db.MessageTemplates.AnyAsync(t => t.Name == shortName))
        {
            var tmpl = new MessageTemplate
            {
                TemplateId = Guid.NewGuid(),
                Name = shortName,
                Description =
                    "Wegen {Freitext} verzögert sich die Weiterfahrt um einige Minuten.",
                SsmlContent =
                    @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <speak version=""1.0""
                           xmlns=""http://www.w3.org/2001/10/synthesis""
                           xml:lang=""de-AT"">
                      <voice name=""de-AT-IngridNeural"">
                        <p>
                          <s>
                            Wegen {Freitext} verzögert sich die Weiterfahrt um einige Minuten.
                          </s>
                        </p>
                      </voice>
                    </speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        const string longName = "Lange Durchsage";

        if (!await db.MessageTemplates.AnyAsync(t => t.Name == longName))
        {
            var tmpl = new MessageTemplate
            {
                TemplateId = Guid.NewGuid(),
                Name = longName,
                Description =
                    "Aufgrund von Bauarbeiten werden die Haltestellen {Location.RefLocationName} und {Location.RefLocationName} heute nicht bedient.\n" +
                    " Stattdessen wird eine Ersatzhaltestelle in {Location.RefLocationName} eingerichtet.\n " +
                    "Die Linie {Route.RouteNr} fährt heute auf Plattform {Platform.PlatformNr} ab.",
                SsmlContent =
                    @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <speak version=""1.0""
                           xmlns=""http://www.w3.org/2001/10/synthesis""
                           xml:lang=""de-AT"">
                      <voice name=""de-AT-IngridNeural"">
                        <p>
                          <s>Aufgrund von Bauarbeiten werden die Haltestellen
                             {Location.RefLocationName} und {Location.RefLocationName}
                             heute nicht bedient.</s>
                          <break time=""400ms""/>
      
                          <s>Stattdessen wird eine Ersatzhaltestelle in
                             {Location.RefLocationName} eingerichtet.</s>
                          <break time=""400ms""/>

                          <s>Die Linie
                            <say-as interpret-as=""cardinal"">{Route.RouteNr}</say-as><break time=""50ms""/>
                            fährt heute auf Plattform
                            <say-as interpret-as=""cardinal"">{Platform.PlatformNr}</say-as><break time=""50ms""/> ab.
                          </s>
                          <break time=""400ms""/>

                          <s>Weitere Informationen zu den aktuellen Fahrplanänderungen erhalten Sie unter
                             <say-as interpret-as=""characters"">www.tts-oevv.at</say-as>.
                          </s>
                        </p>
                      </voice>
                    </speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        const string complexNamesName = "Komplexe Namen";

        if (!await db.MessageTemplates.AnyAsync(t => t.Name == complexNamesName))
        {
            var tmpl = new MessageTemplate
            {
                TemplateId = Guid.NewGuid(),
                Name = complexNamesName,
                Description =
                    "Wegen {Freitext} wird die Fahrt der Linie {Route.RouteNr}zwischen {Location.RefLocationName} und {Location.RefLocationName} als Schienenersatzverkehr geführt. \n" +
                    "Die Haltestellen {Location.RefLocationName}, {Location.RefLocationName} sowie {Location.RefLocationName} entfallen vollständig.",
                SsmlContent =
                    @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <speak version=""1.0""
                           xmlns=""http://www.w3.org/2001/10/synthesis""
                           xml:lang=""de-AT"">
                      <voice name=""de-AT-IngridNeural"">
                        <p>
                          <s>Wegen {Freitext} wird die Fahrt der Linie
                            <say-as interpret-as=""cardinal"">{Route.RouteNr}</say-as><break time=""50ms""/>
                            zwischen {Location.RefLocationName} und {Location.RefLocationName}
                            als Schienenersatzverkehr geführt.</s>
                          <break time=""400ms""/>
      
                          <s>Die Haltestellen
                             {Location.RefLocationName},
                             {Location.RefLocationName}
                             sowie {Location.RefLocationName}
                             entfallen vollständig.</s>
                        </p>
                      </voice>
                    </speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        const string complexNumbersName = "Komplexe Nummern";

        if (!await db.MessageTemplates.AnyAsync(t => t.Name == complexNumbersName))
        {
            var tmpl = new MessageTemplate
            {
                TemplateId = Guid.NewGuid(),
                Name = complexNumbersName,
                Description =
                    "Am 27.10.2025 kommt zu Änderungen im Fahrplan." +
                    "Betroffen sind die Linien {Route.RouteNr}, {Route.RouteNr} und {Route.RouteNr}." +
                    " Die Abfahrt erfolgt nun von Plattform {Platform.PlatformNr} A-B.",
                SsmlContent =
                    @"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <speak version=""1.0""
                               xmlns=""http://www.w3.org/2001/10/synthesis""
                               xml:lang=""de-AT"">
                          <voice name=""de-AT-IngridNeural"">
                            <p>
                              <s xml:lang=""de-AT"">
                                Am <say-as interpret-as=""date"" format=""dmy"">27-10-2025</say-as>
                                kommt es zu Änderungen im Fahrplan.
                              </s>

                              <s>
                                Betroffen sind die Linien
                                <say-as interpret-as=""cardinal"">{Route.RouteNr}</say-as><break time=""100ms""/>,
                                <say-as interpret-as=""cardinal"">{Route.RouteNr}</say-as><break time=""100ms""/>
                                und
                                <say-as interpret-as=""cardinal"">{Route.RouteNr}</say-as><break time=""100ms""/>.
                              </s>

                              <s>
                                <break time=""400ms""/>
                                Die Abfahrt erfolgt heute von Plattform
                                <say-as interpret-as=""cardinal"">{Platform.PlatformNr}</say-as>
                                <break time=""50ms""/>Bereich A bis B.
                              </s>
                            </p>
                          </voice>
                        </speak>"
            };

            db.MessageTemplates.Add(tmpl);
        }

        await db.SaveChangesAsync();
        Console.WriteLine("EfMessageTemplateSeeder: Templates ensured via EF.");
    }
}
