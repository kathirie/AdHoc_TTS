using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AdHoc_SpeechSynthesizer.Controllers
{
    [ApiController]
    [Route("api/ttsmodels")]
    public class TtsModelController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TtsModelController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/TtsModel
        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var models = await _db.TtsModels
                .AsNoTracking()
                .ToListAsync();

            return Ok(models);
        }

        // GET: api/TtsModel/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            var model = await _db.TtsModels.FindAsync(id);
            if (model is null) return NotFound();
            return Ok(model);
        }

        // POST: api/TtsModel
        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] TtsModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            model.ModelId = Guid.NewGuid();
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;

            _db.TtsModels.Add(model);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModel), new { id = model.ModelId }, model);
        }

        // PUT: api/TtsModel/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateModel(Guid id, [FromBody] TtsModel updated)
        {
            var model = await _db.TtsModels.FindAsync(id);
            if (model is null) return NotFound();

            model.Name = updated.Name;
            model.Provider = updated.Provider;
            model.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TtsModel/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteModel(Guid id)
        {
            var model = await _db.TtsModels.FindAsync(id);
            if (model is null) return NotFound();

            _db.TtsModels.Remove(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
