using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;

[ApiController]
[Route("api/[controller]")]
public class TargetTextController : ControllerBase
{
    private readonly ITargetTextService _service;

    public TargetTextController(ITargetTextService service)
    {
        _service = service;
    }

    // /api/targettexts/fronttexts
    [HttpGet("fronttexts")]
    public async Task<IActionResult> GetAllFrontTexts()
    {
        var frontTexts = await _service.GetAllFrontTextsAsync();
        return Ok(frontTexts);
    }
}

