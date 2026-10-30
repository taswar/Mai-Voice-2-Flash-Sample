# Mai-Voice-2-Flash-Sample (AspNet)

An ASP.NET Core minimal API that synthesizes speech using the **MAI-Voice-2 Flash** model hosted on **Azure Speech**, and returns the result as an in-memory MP3 stream — the shape you'd actually use behind a call center agent, IVR system, or conversational assistant.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- An Azure Speech resource (or Microsoft Foundry resource) with **MAI-Voice-2 Flash** deployed

## Required libraries

Already referenced in `Mai-Voice-2-Flash-Sample.AspNetSample.csproj`:

| Package | Version |
| --- | --- |
| `Microsoft.CognitiveServices.Speech` | `1.51.1` |

```powershell
dotnet restore
```

## Configuration

The app reads its Speech key/region via `Microsoft.Extensions.Configuration`, from user-secrets or environment variables. Because this project has no `launchSettings.json`, `dotnet run` runs in the `Production` environment by default — so user-secrets must be loaded explicitly (already done in `Program.cs` via `builder.Configuration.AddUserSecrets<Program>().AddEnvironmentVariables()`).

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
cd AspNet\Mai-Voice-2-Flash-Sample.AspNetSample
dotnet run
```

By default, Kestrel listens on `http://localhost:5000` (no `launchSettings.json` in this project).

## Calling the endpoint

`POST /speak` with a JSON body `{ "text": "..." }`, returns `audio/mpeg`.

**PowerShell** — use `curl.exe` explicitly (not the `curl` alias for `Invoke-WebRequest`), and note that PowerShell doesn't support bash's trailing-`\` line continuation:

```powershell
curl.exe -X POST http://localhost:5000/speak -H "Content-Type: application/json" -d '{"text":"Your order has shipped and will arrive Thursday."}' --output response.mp3
```

Or the more idiomatic PowerShell way:

```powershell
$body = @{ text = "Your order has shipped and will arrive Thursday." } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5000/speak" -Method Post -ContentType "application/json" -Body $body -OutFile response.mp3
```

**bash:**

```bash
curl -X POST http://localhost:5000/speak \
  -H "Content-Type: application/json" \
  -d '{"text":"Your order has shipped and will arrive Thursday."}' \
  --output response.mp3
```

## Troubleshooting

| Error | Cause | Fix |
| --- | --- | --- |
| `Set Speech:Key via user-secrets or environment variables.` | `Speech:Key`/`Speech:Region` not configured | Run the user-secrets commands above, or set the environment variables |
| `Exception with an error code: 0x5` from `SpeechConfig.FromSubscription` | User-secrets weren't loaded because `ASPNETCORE_ENVIRONMENT` isn't `Development` | Already handled in `Program.cs` via explicit `AddUserSecrets`/`AddEnvironmentVariables` — confirm the config values are actually set |
| `curl: (3) URL rejected: Bad hostname` | Bash-style `\` line continuation used in PowerShell | Use `curl.exe` on one line, or PowerShell's backtick `` ` `` continuation |
| `Unsupported voice ...` | Voice name doesn't exist for this locale/model | Use `SpeechSynthesizer.GetVoicesAsync()` to list valid names for your resource |
