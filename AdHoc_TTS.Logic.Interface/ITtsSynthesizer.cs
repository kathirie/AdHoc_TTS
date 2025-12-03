namespace AdHoc_SpeechSynthesizer.Services.Synthesizers;

public interface ITtsSynthesizer : IDisposable
{
    Task<byte[]> SynthesizeToWavAsync(string ssmlContent);
}
