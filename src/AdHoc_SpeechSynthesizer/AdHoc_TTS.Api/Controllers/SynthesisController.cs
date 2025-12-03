using AdHoc_SpeechSynthesizer.Api.Dtos;
using AdHoc_SpeechSynthesizer.Api.Mapper;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/synthesis")]
public class SynthesisController(ISynthesisService service) : ControllerBase
{
    [HttpPost("from-ssml")]
    public async Task<IActionResult> Synthesize([FromBody] SynthesisRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var domainRequest = dto.ToDomain();
        var wavBytes = await service.SynthesizeAsync(domainRequest);

        return File(wavBytes, "audio/wav", "output.wav");
    }

    [HttpPost("from-template")]
    public async Task<IActionResult> SynthesizeFromTemplate([FromBody] SynthesisFromTemplateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var domainRequest = dto.ToDomain();
        var wavBytes = await service.SynthesizeFromTemplateAsync(domainRequest);

        return File(wavBytes, "audio/wav", "output.wav");
    }
}
