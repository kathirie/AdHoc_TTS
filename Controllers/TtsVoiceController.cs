// Controllers/TtsVoiceController.cs
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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


// german voices only
[ApiController]
[Route("api/TtsModels/{modelId:guid}/voices")]
public class TtsModelVoicesController : ControllerBase
{
    private readonly AppDbContext _db;

    public TtsModelVoicesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/TtsModel/{modelId}/voices
    [HttpGet]
    public async Task<IActionResult> GetVoicesForModel(Guid modelId)
    {
        var exists = await _db.TtsModels.AsNoTracking()
            .AnyAsync(m => m.ModelId == modelId);
        if (!exists) return NotFound($"No model found for ID: {modelId}");

        var voices = await _db.TtsVoices.AsNoTracking()
            .Where(v => v.ModelId == modelId && v.IsActive && v.Locale.StartsWith("de"))
            .OrderBy(v => v.DisplayName)
            .Select(v => v)
            .ToListAsync();

        return Ok(voices);
    }
}
