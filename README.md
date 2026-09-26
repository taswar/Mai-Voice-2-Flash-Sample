# Mai-Voice-2-Flash-Sample

Blog Post: [MAI-Voice-2 Flash for .NET Developers — Real-Time Voice on Azure Speech](https://taswar.zeytinsoft.com/mai-voice-2-flash-dotnet-developers/)

Samples that synthesize speech using the **MAI-Voice-2 Flash** model hosted on **Azure Speech**, with Entra ID user-secrets/environment-variable configuration (no hardcoded keys).

This repo contains two runnable projects:

| Project | Type | What it shows |
| --- | --- | --- |
| [`Console/`](Console/) | Console app | Basic synthesis, plain SSML with pacing/pauses, and a Turkish (`tr-TR`) example |
| [`AspNet/`](AspNet/Mai-Voice-2-Flash-Sample.AspNetSample/) | ASP.NET Core minimal API | A `POST /speak` endpoint that returns synthesized audio as an MP3 stream |

See the README inside each subfolder for exact run instructions.

## Prerequisites (both projects)

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- An Azure Speech resource (or Azure AI Foundry resource) with **MAI-Voice-2 Flash** available
- Your Speech resource **key** and **region**

## Known issues / findings from testing

- **Voice name format:** MAI-Voice models use `{locale}-{Name}:MAI-Voice-2-Flash` (e.g. `en-US-Olivia:MAI-Voice-2-Flash`), not the legacy `...Neural` suffix.
- **`mstts:express-as` is not supported.** Attempting to use style values like `empathetic` or `cheerful` returns `invalid_request` from the service — MAI-Voice-2 Flash does not support the classic emotion/style SSML element that legacy Neural voices use. Plain SSML (voice selection, `<break>` for pauses) works fine.
- **Not every locale has every persona.** For example, `tr-TR` doesn't have an `Emel` MAI voice — the actual MAI-Voice-2-Flash voices for `tr-TR` are `Aydın` and `Elif`. Use `SpeechSynthesizer.GetVoicesAsync()` to list what's actually available on your resource before assuming a voice name from documentation or marketing materials.
