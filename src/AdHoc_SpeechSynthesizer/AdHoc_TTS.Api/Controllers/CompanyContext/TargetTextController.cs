using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/[controller]")]
public class TargetTextController(ITargetTextService service) : ControllerBase
{
    [HttpGet("fronttexts")]
    public async Task<IActionResult> GetAllFrontTexts()
    {
        var texts = await service.GetAllFrontTextsAsync();
        return Ok(texts);
    }
}
