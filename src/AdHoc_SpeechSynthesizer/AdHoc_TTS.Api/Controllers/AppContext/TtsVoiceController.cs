using AdHoc_SpeechSynthesizer.Contracts.TtsVoices;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using AdHoc_TTS.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/ttsvoices")]
public class TtsVoiceController(ITtsVoiceService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TtsVoiceDto>>> GetAll(
        [FromQuery] string? locale = null,
        [FromQuery] Guid? modelId = null)
    {
        var voices = await service.GetAllAsync(locale, modelId);
        return Ok(voices.ToDto());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TtsVoiceDto>> GetVoice(Guid id)
    {
        var voice = await service.GetByIdAsync(id);

        if (voice is null)
            return NotFound();

        return Ok(voice.ToDto());
    }
}
