using Dapper;
using Microsoft.Data.SqlClient;


namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public static class MessageTemplateSeeder
{
    private const string schema = "dbo";

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("No connection string configured.");

        using var conn = new SqlConnection(connectionString);

        var insertSql = $@"
                IF NOT EXISTS (
                    SELECT 1 
                    FROM [{schema}].[MessageTemplate]
                    WHERE Name = @Name
                )
                BEGIN
                    INSERT INTO [{schema}].[MessageTemplate]
                        (TemplateId, Name, Description, SSMLContent)
                    VALUES
                        (NEWID(), @Name, @Description, @SSMLContent)
                END";

        // Beispiel 1: Baustelle
        var baustelle = new
        {
            Name = "Baustelle",
            Description = "Aufgrund von Bauarbeiten kann die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} nicht angefahren werden. Die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} wird daher ausgelassen.",
            SSMLContent =
                @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""en-US"">
                      <voice name=""en-US-AvaNeural"">
                        Aufgrund von Bauarbeiten kann die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} nicht angefahren werden.
                        <break />
                        Die Haltestelle {Location.RefLocationName} {Platform.PlatformNr} wird daher ausgelassen.
                      </voice>
                    </speak>"
                            };

        // Beispiel 2: Technische Störungen
        var stoerung = new
        {
            Name = "Technische Störungen",
            Description = "Aufgrund von {Freitext}  <break />kommt es bei {Route.RouteNr} zu Verspätungen.",
            SSMLContent =
                @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""en-US"">
                      <voice name=""en-US-AvaNeural"">
                        Aufgrund von {Freitext} kommt es bei {Route.RouteNr} zu Verspätungen.
                      </voice>
                    </speak>"
                            };

        var inserted1 = await conn.ExecuteAsync(insertSql, baustelle);
        var inserted2 = await conn.ExecuteAsync(insertSql, stoerung);

        Console.WriteLine($"MessageTemplateSeeder: Inserts executed (Baustelle: {inserted1}, Technische Störungen: {inserted2}).");
    }
}
