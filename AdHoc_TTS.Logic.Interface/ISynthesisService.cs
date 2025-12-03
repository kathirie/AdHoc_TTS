using AdHoc_TTS.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces;

public interface ISynthesisService
{
    // Synthesizes speech based on the given request and returns a WAV audio stream as bytes.
    Task<byte[]> SynthesizeAsync(SynthesisRequest request);

    // Synthesizes speech based on the given template and inputdata, creates ssml and returns a WAV audio stream as bytes.
    Task<byte[]> SynthesizeFromTemplateAsync(SynthesisFromTemplateRequest request);
}