using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/[controller]")]
public class PlatformController(IPlatformService service) : ControllerBase
{
    [HttpGet("platformnumbers")]
    public async Task<IActionResult> GetAllPlatformNumbers()
    {
        var numbers = await service.GetAllPlatformNumbersAsync();
        return Ok(numbers);
    }
}
