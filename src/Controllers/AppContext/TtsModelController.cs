using AdHoc_SpeechSynthesizer.Models.AppContext;
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

    // POST: api/ttsmodels
    [HttpPost]
    public async Task<IActionResult> CreateModel([FromBody] TtsModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(model);
        return CreatedAtAction(nameof(GetModel), new { id = created.ModelId }, created);
    }

    // // PUT: api/ttsmodels/{id}
    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> UpdateModel(Guid id, [FromBody] TtsModel updated)
    // {
    //     if (!ModelState.IsValid) return BadRequest(ModelState);
    // 
    //     var success = await _service.UpdateAsync(id, updated);
    //     return success ? NoContent() : NotFound();
    // }
    // 
    // // DELETE: api/models/{id}
    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> DeleteModel(Guid id)
    // {
    //     var success = await _service.DeleteAsync(id);
    //     return success ? NoContent() : NotFound();
    // }
}
