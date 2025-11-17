using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers.CompanyContext;


    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _service;

        public RouteController(IRouteService service)
        {
            _service = service;
        }

        // /api/routes/RouteNrs
        [HttpGet("RouteNrs")]
        public async Task<IActionResult> GetAllRouteNumbers()
        {
            var routeNumbers = await _service.GetAllRouteNumbersAsync();
            return Ok(routeNumbers);
        }
    }

