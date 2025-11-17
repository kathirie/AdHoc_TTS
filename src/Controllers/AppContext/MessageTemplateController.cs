using AdHoc_SpeechSynthesizer.Common.Templating;
using AdHoc_SpeechSynthesizer.Models.Responses;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[Route("api/messagetemplates")]
public class MessageTemplateController : ControllerBase
{
    private readonly IMessageTemplateService _service;
    public MessageTemplateController(IMessageTemplateService service) => _service = service;


    // api/messagetemplates
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());


    // api/messagetemplates/id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessageTemplate(Guid id)
    {
        var template = await _service.GetByIdAsync(id);
        return template is null ? NotFound() : Ok(template);
    }

    // api/messagetemplates/{id}/placeholders
    [HttpGet("{id:guid}/placeholders")]
    public async Task<IActionResult> GetPlaceholders(Guid id)
    {

        var placeholders = await _service.GetPlaceholdersByIdAsync(id);
        return placeholders is null ? NotFound() : Ok(placeholders);
    }
}
