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

    // GET: api/TargetText?controlCenterId=CC01
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? controlCenterId = null)
    {
        var result = await _service.GetAllAsync(controlCenterId);
        return Ok(result);
    }

    // GET: api/TargetText/ControllCenter/Version/TextNr
    [HttpGet("{controlCenterId}/{versionNr:int}/{targetTextNr:int}")]
    public async Task<IActionResult> GetByKey(
        string controlCenterId,
        int versionNr,
        int targetTextNr)
    {
        var text = await _service.GetByKeyAsync(controlCenterId, versionNr, targetTextNr);
        return text is null ? NotFound() : Ok(text);
    }
}

