using AdHoc_SpeechSynthesizer.Services.CompanyContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;

[ApiController]
[Route("api/[controller]")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _service;

    public PlatformController(IPlatformService service)
    {
        _service = service;
    }

    // GET /api/platforms/names
    [HttpGet("names")]
    public async Task<IActionResult> GetAllPlatformNames()
    {
        var names = await _service.GetAllPlatformNamesAsync();
        return Ok(names);
    }

    // GET: api/Platform?controlCenterId=CC01&locationTypeNr=5&locationNr=123
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? controlCenterId = null,
        [FromQuery] int? locationTypeNr = null,
        [FromQuery] int? locationNr = null)
    {
        var result = await _service.GetAllAsync(controlCenterId, locationTypeNr, locationNr);
        return Ok(result);
    }

    // GET: api/Platform/location/CC01/1/10/123
    // -> all platforms at this location
    [HttpGet("location/{controlCenterId}/{versionNr:int}/{locationTypeNr:int}/{locationNr:int}")]
    public async Task<IActionResult> GetByLocation(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr)
    {
        var result = await _service.GetByLocationAsync(controlCenterId, versionNr, locationTypeNr, locationNr);
        return Ok(result);
    }

    // GET: api/Platform/ControlCenter/Version/LocationType/LocatioNr/Platform
    [HttpGet("{controlCenterId}/{versionNr:int}/{locationTypeNr:int}/{locationNr:int}/{platformNr:int}")]
    public async Task<IActionResult> GetByKey(
        string controlCenterId,
        int versionNr,
        int locationTypeNr,
        int locationNr,
        int platformNr)
    {
        var platform = await _service.GetByKeyAsync(controlCenterId, versionNr, locationTypeNr, locationNr, platformNr);
        return platform is null ? NotFound() : Ok(platform);
    }
}