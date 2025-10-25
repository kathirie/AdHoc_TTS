using AdHoc_SpeechSynthesizer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Controllers
{
    [ApiController]
    [Route("api/ttsvoices")] // <- explicit route so you hit /api/voices
    public class TtsVoiceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TtsVoiceController(AppDbContext db) => _db = db;

        // GET: /api/voices
        [HttpGet]
        public async Task<IActionResult> GetAllVoices()
        {
            var voices = await _db.TtsVoices.AsNoTracking().ToListAsync();
            return Ok(voices);
        }

        // GET: /api/voices/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetVoice(Guid id)
        {
            var voice = await _db.TtsVoices.FindAsync(id);
            return voice is null ? NotFound() : Ok(voice);
        }
    }
}
