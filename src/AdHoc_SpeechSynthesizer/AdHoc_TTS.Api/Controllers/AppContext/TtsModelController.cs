using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/ttsmodels")]
public class TtsModelController(ITtsModelService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllModels()
    {
        var models = await service.GetAllAsync();
        return Ok(models);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetModel(Guid id)
    {
        var model = await service.GetByIdAsync(id);

        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }


}
