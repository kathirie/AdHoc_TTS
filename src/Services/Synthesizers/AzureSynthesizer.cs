using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace AdHoc_SpeechSynthesizer.Services.Synthesizers;

public class AzureSynthesizer : ITtsSynthesizer
{
    private readonly SpeechConfig _config;

    public AzureSynthesizer(string key, string region, string voiceName)
    {
        _config = SpeechConfig.FromSubscription(key, region);
        _config.SpeechSynthesisVoiceName = voiceName;
    }

    public async Task<byte[]> SynthesizeToWavAsync(string ssmlContent)
    {
        if (string.IsNullOrWhiteSpace(ssmlContent))
            throw new ArgumentException("SSML content cannot be empty.", nameof(ssmlContent));

        _config.SetSpeechSynthesisOutputFormat(
            Microsoft.CognitiveServices.Speech.SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm);

        using var synth = new SpeechSynthesizer(_config);
        var result = await synth.SpeakSsmlAsync(ssmlContent).ConfigureAwait(false);

        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            var audio = result.AudioData;
            if (audio == null || audio.Length == 0)
                throw new InvalidOperationException("Synthesized audio is empty.");
            return audio;
        }

        var details = SpeechSynthesisCancellationDetails.FromResult(result);
        throw new InvalidOperationException(
            $"Azure synthesis failed: {result.Reason}. " +
            $"CancellationReason={details.Reason}, ErrorCode={details.ErrorCode}, Details={details.ErrorDetails}");
    }

    public void Dispose() { }
}
