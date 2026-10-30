# MAI-Voice-2 Flash C# Text-to-Speech Samples

![MAI-Voice-2 Flash for .NET Developers](https://taswar.zeytinsoft.com/wp-content/uploads/2026/07/MAI-Voice-2-Flash-for-.NET-Developers.png)

[![Build](https://github.com/taswar/Mai-Voice-2-Flash-Sample/actions/workflows/dotnet.yml/badge.svg)](https://github.com/taswar/Mai-Voice-2-Flash-Sample/actions/workflows/dotnet.yml)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen)](CONTRIBUTING.md)

- 📖 Full walkthrough: [MAI-Voice-2 Flash for .NET Developers — Real-Time Voice on Azure Speech](https://taswar.zeytinsoft.com/mai-voice-2-flash-dotnet-developers/)
- ⭐ If this sample helps you, star the repo so other .NET developers can find it.
- 🔁 Related: [MAI-Image-2.5-Pro C# Sample](https://github.com/taswar/MAI-Image-2.5-Pro-Sample), Microsoft's image model on Foundry
- For more C# samples using other AI models, see [AI models for C# developers](https://taswar.zeytinsoft.com/foundry-ai-models-csharp/)
- For my free ebook on prompt engineering, see [Prompt Engineering for .NET Developers](https://github.com/taswar/PromptEngineeringForDotNetDevelopers)

.NET 10 samples that turn text into speech (text-to-speech) with the **MAI-Voice-2 Flash** model hosted on **Azure Speech**, using the Speech SDK. The Speech key and region are read from user secrets or environment variables, so no keys are hard-coded.

This repo contains two runnable projects:

| Project | Type | What it shows |
| --- | --- | --- |
| [`Console/`](Console/) | Console app | Basic synthesis, plain SSML with pacing/pauses, and a Turkish (`tr-TR`) example |
| [`AspNet/`](AspNet/Mai-Voice-2-Flash-Sample.AspNetSample/) | ASP.NET Core minimal API | A `POST /speak` endpoint that returns synthesized audio as an MP3 stream |

See the README inside each subfolder for exact run instructions.

## Quick start

```powershell
git clone https://github.com/taswar/Mai-Voice-2-Flash-Sample.git
cd Mai-Voice-2-Flash-Sample/Console
dotnet user-secrets set "Speech:Key" "YOUR_SPEECH_KEY"
dotnet user-secrets set "Speech:Region" "eastus"
dotnet run
```

## Prerequisites (both projects)

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- An Azure Speech resource (or Microsoft Foundry resource) with **MAI-Voice-2 Flash** available
- Your Speech resource **key** and **region**

## Known issues / findings from testing

- **Voice name format:** MAI-Voice models use `{locale}-{Name}:MAI-Voice-2-Flash` (e.g. `en-US-Olivia:MAI-Voice-2-Flash`), not the legacy `...Neural` suffix.
- **`mstts:express-as` is not supported.** Attempting to use style values like `empathetic` or `cheerful` returns `invalid_request` from the service — MAI-Voice-2 Flash does not support the classic emotion/style SSML element that legacy Neural voices use. Plain SSML (voice selection, `<break>` for pauses) works fine.
- **Not every locale has every persona.** For example, `tr-TR` doesn't have an `Emel` MAI voice — the actual MAI-Voice-2-Flash voices for `tr-TR` are `Aydın` and `Elif`. Use `SpeechSynthesizer.GetVoicesAsync()` to list what's actually available on your resource before assuming a voice name from documentation or marketing materials.

## Project layout

```
Console/                                     Console app: plain text, SSML with a pause, and Turkish
AspNet/Mai-Voice-2-Flash-Sample.AspNetSample/  Minimal API: POST /speak returns an MP3
CONTRIBUTING.md                              How to contribute
.github/workflows/dotnet.yml                 Build workflow for both projects
LICENSE                                      MIT license
```

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
