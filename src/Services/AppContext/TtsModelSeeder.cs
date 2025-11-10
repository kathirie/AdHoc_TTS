using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public static class TtsModelSeeder
{
    // Seeds the TtsModel table with default entries for Azure and System.Speech.
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("No connection string configured.");

        await SeedModelAsync(connectionString);
    }

    private static async Task SeedModelAsync(string connectionString)
    {
        const string sql = @"
                IF NOT EXISTS (SELECT 1 FROM [dbo].[TtsModel] WHERE Provider = 'azure')
                BEGIN
                    INSERT INTO [dbo].[TtsModel] (ModelId, Provider, Name, IsActive, CreatedAt, UpdatedAt)
                    VALUES (NEWID(), 'azure', 'Azure Neural TTS', 1, SYSUTCDATETIME(), SYSUTCDATETIME())
                END

                IF NOT EXISTS (SELECT 1 FROM [dbo].[TtsModel] WHERE Provider = 'system.speech')
                BEGIN
                    INSERT INTO [dbo].[TtsModel] (ModelId, Provider, Name, IsActive, CreatedAt, UpdatedAt)
                    VALUES (NEWID(), 'system.speech', 'System.Speech', 1, SYSUTCDATETIME(), SYSUTCDATETIME())
                END
                ";

        using var conn = new SqlConnection(connectionString);
        var affected = await conn.ExecuteAsync(sql);

        Console.WriteLine($"TtsModelSeeder: Default models ensured (affected {affected} rows).");
    }
}
