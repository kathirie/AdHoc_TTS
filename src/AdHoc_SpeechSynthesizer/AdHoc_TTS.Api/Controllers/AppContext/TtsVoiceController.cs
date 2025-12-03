using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/ttsvoices")]
public class TtsVoiceController(ITtsVoiceService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? locale = null, [FromQuery] Guid? modelId = null)
    {
        var voices = await service.GetAllAsync(locale, modelId);
        return Ok(voices);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVoice(Guid id)
    {
        var voice = await service.GetByIdAsync(id);

        if (voice is null)
        {
            return NotFound();
        }

        return Ok(voice);
    }
}
