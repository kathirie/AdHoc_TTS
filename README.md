# TTS API (ASP.NET Core)

A Text-to-Speech REST API built with **ASP.NET Core**, supporting:

-   Azure Speech Services (via **Microsoft Foundry Tools**)
-   Windows **System.Speech** (SAPI 5.4)
-   Voice/model/template seeding
-   EF Core + SQL Server

------------------------------------------------------------------------

## Development

1.  Set **User Secrets**:

        SPEECH_KEY
        SPEECH_REGION

2.  Configure **ConnectionStrings** (AppData + CompanyData).

3.  Run the API:

        dotnet run

    (see `launchSettings.json` for ports)

------------------------------------------------------------------------

## Endpoints Overview

The API exposes two synthesis endpoints:

-   `POST /api/synthesis/from-ssml`\
    → Synthesize raw SSML.

-   `POST /api/synthesis/from-template`\
    → Synthesize speech using a predefined template with placeholders.

------------------------------------------------------------------------

## 1. SSML Synthesis Example

**Endpoint:** `POST /api/synthesis/from-ssml`

``` http
POST https://localhost:7275/api/synthesis/from-ssml
Content-Type: application/json
Accept: audio/wav

{
  "ssmlContent": "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<speak version=\"1.0\"\n       xmlns=\"http://www.w3.org/2001/10/synthesis\"\n       xml:lang=\"de-AT\">\n  <voice name=\"de-AT-JonasNeural\">\n    <p>\n      <s>Nächster Halt: Linz Hauptbahnhof.</s><break time=\"400ms\"/>\n      <s>Sie haben Anschluss zu den Linien\n          1<break time=\"300ms\"/>,\n          2<break time=\"300ms\"/>\n          und 43</s>\n    </p>\n  </voice>\n</speak>",
  "modelId": "D229008E-A9B5-410C-B794-274763DACA4E",
  "voiceId": null
}
```

------------------------------------------------------------------------

## 2. Template-Based Synthesis Example

**Endpoint:** `POST /api/synthesis/from-template`

``` http
POST https://localhost:7275/api/synthesis/from-template
Content-Type: application/json

{
  "templateId": "1a24c180-f199-4caf-8d3d-4e39f1c46937",
  "modelId": "D229008E-A9B5-410C-B794-274763DACA4E",
  "voiceId": null,
  "refLocationNames": ["Hauptbahnhof", "Kärntnerstraße"],
  "platformNumbers": ["2", "15"],
  "routeNrs": null,
  "frontTexts": null,
  "freeTexts": null
}
```

------------------------------------------------------------------------
