using AdHoc_SpeechSynthesizer.Contracts.TtsModels;
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
    public async Task<ActionResult<IEnumerable<TtsModelDto>>> GetAllModels()
    {
        var models = await service.GetAllAsync();
        return Ok(models.ToDto());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TtsModelDto>> GetModel(Guid id)
    {
        var model = await service.GetByIdAsync(id);

        if (model is null)
            return NotFound();

        return Ok(model.ToDto());
    }
}
