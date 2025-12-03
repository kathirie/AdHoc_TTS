
using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces;

public interface ISynthesisService
{
    Task<byte[]> SynthesizeAsync(SynthesisRequest request);

    Task<byte[]> SynthesizeFromTemplateAsync(SynthesisFromTemplateRequest request);
}