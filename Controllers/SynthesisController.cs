using AdHoc_SpeechSynthesizer.Models.Requests;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers;

[ApiController]
[Route("api/synthesis")]
public class SynthesisController : ControllerBase
{
    private readonly ISynthesisService _service;

    public SynthesisController(ISynthesisService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Synthesize([FromBody] SynthesisRequest request)
    {
        try
        {
            var wavBytes = await _service.SynthesizeAsync(request);
            return File(wavBytes, "audio/wav", "output.wav");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
