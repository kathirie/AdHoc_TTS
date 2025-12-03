using System.Speech.Synthesis;

namespace AdHoc_SpeechSynthesizer.Services.Synthesizers;

public class WindowsSynthesizer(string voiceName) : ITtsSynthesizer
{
    private readonly string _voiceName = voiceName ?? throw new ArgumentNullException(nameof(voiceName));

    public Task<byte[]> SynthesizeToWavAsync(string ssmlContent)
    {
        using var synth = new SpeechSynthesizer();
        synth.SelectVoice(_voiceName);

        using var ms = new MemoryStream();
        synth.SetOutputToWaveStream(ms);
        synth.SpeakSsml(ssmlContent);

        return Task.FromResult(ms.ToArray());
    }

    public void Dispose() { }
}
