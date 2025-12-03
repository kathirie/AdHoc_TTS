using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/[controller]")]
public class LocationController(ILocationService service) : ControllerBase
{
    [HttpGet("RefLocationNames")]
    public async Task<IActionResult> GetAllRefLocationNames()
    {
        var names = await service.GetAllRefLocationNamesAsync();
        return Ok(names);
    }
}
