# Contributing

Thanks for helping improve these samples. They are two small .NET 10 projects that turn text into speech with MAI-Voice-2 Flash on Azure Speech, using the Speech SDK: a console app and an ASP.NET Core minimal API. Contributions that make them clearer, more correct, or more useful to other .NET developers are welcome.

## Ways to contribute

- **Report a bug.** A sample fails, or a README doesn't match what the code does.
- **Fix the docs.** Typos, unclear steps, or a missing troubleshooting case.
- **Improve the samples.** For example, another locale, saving audio to a file in the console app, or streaming audio from the API.
- **Share findings.** MAI voices differ from the classic Neural voices. If you found a voice name, locale or SSML element that does or doesn't work, an issue helps others. The root README keeps a list of known issues.

For anything larger than a small fix, open an issue first so we can agree on the approach before you write code.

## Set up

1. Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
2. Clone the repo and build both projects:

   ```bash
   git clone https://github.com/taswar/Mai-Voice-2-Flash-Sample.git
   cd Mai-Voice-2-Flash-Sample
   dotnet build Console/Mai-Voice-2-Flash-Sample.csproj
   dotnet build AspNet/Mai-Voice-2-Flash-Sample.AspNetSample/Mai-Voice-2-Flash-Sample.AspNetSample.csproj
   ```

3. To run them, you need an Azure Speech resource with MAI-Voice-2 Flash available. Set `Speech:Key` and `Speech:Region` with user secrets, as described in the [Console README](Console/README.md#configuration). Both projects share the same user secrets ID, so you only set them once.

## Guidelines

- **Never commit secrets.** No Speech keys, regions tied to your account, or tokens in code, config files or screenshots. Settings belong in user secrets or environment variables.
- **Use MAI voice names.** MAI voices use `{locale}-{Name}:MAI-Voice-2-Flash`, not the `...Neural` suffix. Check a name with `GetVoicesAsync()` before you add it.
- **Keep SSML plain.** MAI-Voice-2 Flash rejects `mstts:express-as`. Use voice selection and `<break>` only, or explain in the pull request what you tested.
- **Keep the error output.** Each call prints the cancellation reason and error details on failure, and the API returns them as a problem response. Keep that for any new call.
- **Keep them samples.** The code should stay short and readable in one `Program.cs` per project. Prefer clarity over abstraction, and add a dependency only when it clearly helps.
- **Match the existing style.** Target `net10.0`, keep nullable reference types on, and follow the layout already in each `Program.cs`.
- **Keep the READMEs in sync.** If you change voices, settings, endpoints or output, update the README for that project, and the known issues in the root README if needed.
- **One change per pull request.** Small, focused pull requests are easier to review and merge.

## Pull request checklist

- [ ] Both projects build with no new warnings.
- [ ] Any sample you changed runs against your own Speech resource, or you explain why you couldn't run it.
- [ ] No secrets or personal data are in the diff.
- [ ] The READMEs reflect any change in voices, settings or output.
- [ ] The pull request description says what changed and why.

The build workflow builds both projects on every pull request and has to pass before a merge.

## Reporting a bug

Open an issue and include:

- Which project (console or API) and which step failed, and what you expected.
- The cancellation reason, error code and error details, with any keys removed.
- The voice name and locale you used.
- Your .NET SDK version (`dotnet --version`) and operating system.

## Security issues

Please don't report security problems in a public issue. Use **Security → Report a vulnerability** on the repo to send a private report.

## License

By contributing, you agree that your contributions are licensed under the [MIT License](LICENSE) that covers this project.
