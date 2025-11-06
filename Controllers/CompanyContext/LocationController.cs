using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;
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

        // GET: api/Location?controlCenterId=CC01&locationTypeNr=5
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? controlCenterId = null,
            [FromQuery] int? locationTypeNr = null)
        {
            var result = await _service.GetAllAsync(controlCenterId, locationTypeNr);
            return Ok(result);
        }

        // GET: api/Location/{controlCenterId}/{versionNr}/{locationTypeNr}/{locationNr}
        [HttpGet("{controlCenterId}/{versionNr:int}/{locationTypeNr:int}/{locationNr:int}")]
        public async Task<IActionResult> GetByKey(
            string controlCenterId,
            int versionNr,
            int locationTypeNr,
            int locationNr)
        {
            var location = await _service.GetByKeyAsync(controlCenterId, versionNr, locationTypeNr, locationNr);
            return location is null ? NotFound() : Ok(location);
        }
    }

