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

        // GET: api/Route?controlCenterId=CC01&routeNr=10&routeVariant=A
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? controlCenterId = null,
            [FromQuery] int? routeNr = null,
            [FromQuery] string? routeVariant = null)
        {
            var routes = await _service.GetAllAsync(controlCenterId, routeNr, routeVariant);
            return Ok(routes);
        }

        // GET: api/Route/ControlCenter/Version/RouteNr/Variant
        [HttpGet("{controlCenterId}/{versionNr:int}/{routeNr:int}/{routeVariant}")]
        public async Task<IActionResult> GetByKey(
            string controlCenterId,
            int versionNr,
            int routeNr,
            string routeVariant)
        {
            var route = await _service.GetByKeyAsync(controlCenterId, versionNr, routeNr, routeVariant);
            return route is null ? NotFound() : Ok(route);
        }
    }

