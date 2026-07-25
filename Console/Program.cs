using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

var speechKey = config["Speech:Key"]
    ?? throw new InvalidOperationException("Set Speech:Key via user-secrets.");
var speechRegion = config["Speech:Region"]
    ?? throw new InvalidOperationException("Set Speech:Region via user-secrets.");

var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

// Voice name follows Azure Speech naming convention for MAI-Voice models:
// {locale}-{Name}:MAI-Voice-2-Flash. See the Foundry model catalog / Speech
// Studio voice gallery for the full list of names available per locale.
speechConfig.SpeechSynthesisVoiceName = "en-US-Olivia:MAI-Voice-2-Flash";

// Output directly to the default speaker. Swap AudioConfig for a file or
// in-memory stream if you're serving this from a web API instead of a console app.
using var audioConfig = AudioConfig.FromDefaultSpeakerOutput();
using var synthesizer = new SpeechSynthesizer(speechConfig, audioConfig);

var text = "Thanks for calling. I can help you check your order status, " +
           "process a return, or connect you with a specialist. What do you need today?";

Console.WriteLine("Synthesizing...");

using var result = await synthesizer.SpeakTextAsync(text);

if (result.Reason == ResultReason.SynthesizingAudioCompleted)
{
    Console.WriteLine("Done. Audio played through default speaker.");
}
else if (result.Reason == ResultReason.Canceled)
{
    var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
    Console.WriteLine($"Canceled: {cancellation.Reason}");

    if (cancellation.Reason == CancellationReason.Error)
    {
        Console.WriteLine($"Error code: {cancellation.ErrorCode}");
        Console.WriteLine($"Error details: {cancellation.ErrorDetails}");
    }
}

var ssml = """
    <speak version="1.0" xmlns="http://www.w3.org/2001/10/synthesis" xml:lang="en-US">
        <voice name="en-US-Olivia:MAI-Voice-2-Flash">
            I'm sorry to hear your order arrived damaged. 
             <break time="300ms"/>
            Let's get that sorted out right away.
        </voice>
    </speak>
    """;

using var ssmlResult = await synthesizer.SpeakSsmlAsync(ssml);

//play the result
if (ssmlResult.Reason == ResultReason.SynthesizingAudioCompleted)
{
    Console.WriteLine("Done. Audio played through default speaker.");
}
else if (ssmlResult.Reason == ResultReason.Canceled)
{
    var cancellation = SpeechSynthesisCancellationDetails.FromResult(ssmlResult);
    Console.WriteLine($"Canceled: {cancellation.Reason}");

    if (cancellation.Reason == CancellationReason.Error)
    {
        Console.WriteLine($"Error code: {cancellation.ErrorCode}");
        Console.WriteLine($"Error details: {cancellation.ErrorDetails}");
    }
}

var ssmlTurkish = """
    <speak version="1.0" xmlns="http://www.w3.org/2001/10/synthesis" xml:lang="tr-TR">
        <voice name="tr-TR-Elif:MAI-Voice-2-Flash">
            Siparişinizin hasarlı ulaştığını duyduğuma üzüldüm. Hemen bir iade işlemi başlatıyorum, birkaç saniye sürecek.
        </voice>
    </speak>
    """;

using var ssmlResultTr = await synthesizer.SpeakSsmlAsync(ssmlTurkish);

//play the result
if (ssmlResultTr.Reason == ResultReason.SynthesizingAudioCompleted)
{
    Console.WriteLine("Done. Audio played through default speaker.");
}
else if (ssmlResultTr.Reason == ResultReason.Canceled)
{
    var cancellation = SpeechSynthesisCancellationDetails.FromResult(ssmlResultTr);
    Console.WriteLine($"Canceled: {cancellation.Reason}");

    if (cancellation.Reason == CancellationReason.Error)
    {
        Console.WriteLine($"Error code: {cancellation.ErrorCode}");
        Console.WriteLine($"Error details: {cancellation.ErrorDetails}");
    }
}