using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;


    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        // api/Location/RefLocationNames
        [HttpGet("RefLocationNames")]
        public async Task<IActionResult> GetAllRefLocationNames()
        {
            var names = await _service.GetAllRefLocationNamesAsync();
            return Ok(names);
        }
}

