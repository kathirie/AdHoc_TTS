using AdHoc_SpeechSynthesizer.Models.Requests;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces;

public interface ISynthesisService
{
    /// <summary>
    /// Synthesizes speech based on the given request and returns a WAV audio stream as bytes.
    /// </summary>
    Task<byte[]> SynthesizeAsync(SynthesisRequest request);
}