using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

var builder = WebApplication.CreateBuilder(args);

// Load user-secrets/environment variables explicitly — WebApplication only
// auto-loads user-secrets when ASPNETCORE_ENVIRONMENT=Development, and this
// project has no launchSettings.json to set that when running `dotnet run`.
builder.Configuration
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var app = builder.Build();

app.MapPost("/speak", async (SpeakRequest request, IConfiguration config) =>
{
    var speechKey = config["Speech:Key"]
        ?? throw new InvalidOperationException("Set Speech:Key via user-secrets or environment variables.");
    var speechRegion = config["Speech:Region"]
        ?? throw new InvalidOperationException("Set Speech:Region via user-secrets or environment variables.");

    var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

    speechConfig.SpeechSynthesisVoiceName = "en-US-Olivia:MAI-Voice-2-Flash";
    speechConfig.SetSpeechSynthesisOutputFormat(
        SpeechSynthesisOutputFormat.Audio24Khz96KBitRateMonoMp3);

    // No audio config = in-memory result instead of playing to a device
    using var synthesizer = new SpeechSynthesizer(speechConfig, null);
    using var result = await synthesizer.SpeakTextAsync(request.Text);

    if (result.Reason != ResultReason.SynthesizingAudioCompleted)
    {
        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
        return Results.Problem($"Synthesis failed: {cancellation.ErrorDetails}");
    }

    return Results.File(result.AudioData, "audio/mpeg");
});

app.Run();

record SpeakRequest(string Text);
