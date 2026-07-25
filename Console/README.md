# Mai-Voice-2-Flash-Sample (Console)

A minimal .NET console app that synthesizes speech using the **MAI-Voice-2 Flash** model hosted on **Azure Speech**.

Demonstrates:

1. Basic text-to-speech synthesis, played through the default speaker.
2. Plain SSML with a `<break>` pause for natural-sounding pacing.
3. The same SSML pattern in Turkish (`tr-TR`).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- An Azure Speech resource (or Azure AI Foundry resource) with **MAI-Voice-2 Flash** deployed

## Required libraries

Already referenced in `Mai-Voice-2-Flash-Sample.csproj`:

| Package | Version |
| --- | --- |
| `Microsoft.CognitiveServices.Speech` | `1.51.1` |
| `Microsoft.Extensions.Configuration.EnvironmentVariables` | `10.0.10` |
| `Microsoft.Extensions.Configuration.UserSecrets` | `10.0.10` |

```powershell
dotnet restore
```

## Configuration

The app reads its Speech key/region via `Microsoft.Extensions.Configuration`, from user-secrets or environment variables:

| Setting | Required | Description |
| --- | --- | --- |
| `Speech:Key` | Yes | Your Azure Speech resource key |
| `Speech:Region` | Yes | Your Azure Speech resource region, e.g. `eastus` |

### Option A — user-secrets (recommended for local dev)

```powershell
dotnet user-secrets init
dotnet user-secrets set "Speech:Key" "YOUR_SPEECH_KEY"
dotnet user-secrets set "Speech:Region" "eastus"
```

### Option B — environment variables

```powershell
$env:Speech__Key = "YOUR_SPEECH_KEY"
$env:Speech__Region = "eastus"
```

## How to run

```powershell
cd Console
dotnet run
```

You should hear three synthesized clips play through your default audio output: a plain-text greeting, an English SSML example with a pause, and a Turkish SSML example.

## Voice names

MAI-Voice models follow the naming convention `{locale}-{Name}:MAI-Voice-2-Flash` (e.g. `en-US-Olivia:MAI-Voice-2-Flash`, `tr-TR-Elif:MAI-Voice-2-Flash`). Available names vary per locale and per resource — list them at runtime instead of guessing:

```csharp
var voicesResult = await synthesizer.GetVoicesAsync();
foreach (var voice in voicesResult.Voices)
{
    Console.WriteLine($"{voice.ShortName} ({voice.Locale})");
}
```

## Troubleshooting

| Error | Cause | Fix |
| --- | --- | --- |
| `Set Speech:Key via user-secrets.` | `Speech:Key`/`Speech:Region` not configured | Run the user-secrets commands above, or set the environment variables |
| `Exception with an error code: 0x5` from `SpeechConfig.FromSubscription` | Key or region is null/empty | Same as above — confirm the config values are actually being loaded |
| `Unsupported voice ...` | Voice name doesn't exist for this locale/model | Call `GetVoicesAsync()` and use an exact `ShortName` from the result |
| `Bad MAI Request: {"error":{"code":"invalid_request"...` when using SSML | Using `<mstts:express-as style="...">` | Not supported by MAI-Voice-2 Flash — remove the `express-as` element and use plain text/`<break>` only |
