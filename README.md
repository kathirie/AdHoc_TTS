# TTS API (ASP.NET Core)

- ASP.NET REST API exposing TTS with Azure Speech (Microsoft Foundry Tools) and System.Speech (Windows SAPI 5.4)
- Seeding for voices and models
- EF Core + SQL Server

## Dev
- Set User Secrets: `SPEECH_KEY` and `SPEECH_REGION` for Azure Speech Services
- Set ConnectionStrings for AppData and CompanyData DB
- Run: `dotnet run` (see launchSettings.json ports)
