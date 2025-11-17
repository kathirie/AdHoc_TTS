using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[Route("api/ttsmodels")]
public class TtsModelController : ControllerBase
{
    private readonly ITtsModelService _service;

    public TtsModelController(ITtsModelService service)
    {
        _service = service;
    }

    // api/ttsmodels
    [HttpGet]
    public async Task<IActionResult> GetAllModels()
    {
        var models = await _service.GetAllAsync();
        return Ok(models);
    }

    // api/ttsmodels/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetModel(Guid id)
    {
        var model = await _service.GetByIdAsync(id);
        return model is null ? NotFound() : Ok(model);
    }
}
