using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[Route("api/messagetemplates")]
public class MessageTemplateController : ControllerBase
{
    private readonly IMessageTemplateService _service;
    public MessageTemplateController(IMessageTemplateService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessageTemplate(Guid id)
    {
        var voice = await _service.GetByIdAsync(id);
        return voice is null ? NotFound() : Ok(voice);
    }
}
