using AdHoc_SpeechSynthesizer.Models.Requests;

namespace AdHoc_SpeechSynthesizer.Services.Interfaces
{
    public interface IMessageTemplateRenderService
    {
        Task<string> RenderSsmlAsync(SynthesizeFromTemplateRequest request);
    }
}
