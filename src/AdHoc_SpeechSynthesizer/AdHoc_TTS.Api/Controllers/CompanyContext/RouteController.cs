using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/[controller]")]
public class RouteController(IRouteService service) : ControllerBase
{
    [HttpGet("RouteNrs")]
    public async Task<IActionResult> GetAllRouteNumbers()
    {
        var numbers = await service.GetAllRouteNumbersAsync();
        return Ok(numbers);
    }
}
