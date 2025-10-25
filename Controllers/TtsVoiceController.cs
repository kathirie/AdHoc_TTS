// Controllers/TtsVoiceController.cs
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ttsvoices")]
public class TtsVoiceController : ControllerBase
{
    private readonly ITtsVoiceService _service;
    public TtsVoiceController(ITtsVoiceService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? locale = null, [FromQuery] string? provider = null)
        => Ok(await _service.GetAllAsync(locale, provider));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVoice(Guid id)
    {
        var voice = await _service.GetByIdAsync(id);
        return voice is null ? NotFound() : Ok(voice);
    }
}
