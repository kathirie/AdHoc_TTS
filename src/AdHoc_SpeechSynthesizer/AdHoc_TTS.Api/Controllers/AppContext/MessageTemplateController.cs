using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using AdHoc_TTS.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Controllers;

namespace AdHoc_SpeechSynthesizer.Controllers.AppContext;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/messagetemplates")]
public class MessageTemplateController(IMessageTemplateService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var templates = await service.GetAllAsync();
        return Ok(templates);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessageTemplate(Guid id)
    {
        var template = await service.GetByIdAsync(id);

        if (template is null)
        {
            return NotFound();
        }

        return Ok(template);
    }

    [HttpGet("{id:guid}/placeholders")]
    public async Task<IActionResult> GetPlaceholders(Guid id)
    {
        var placeholders = await service.GetPlaceholdersByIdAsync(id);

        if (placeholders is null)
        {
            return NotFound();
        }

        return Ok(placeholders.ToDto());
    }
}
