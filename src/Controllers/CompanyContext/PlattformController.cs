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

    // /api/platforms/platformnumbers
    [HttpGet("platformnumbers")]
    public async Task<IActionResult> GetAllPlatformNumbers()
    {
        var names = await _service.GetAllPlatformNumbersAsync();
        return Ok(names);
    }
}