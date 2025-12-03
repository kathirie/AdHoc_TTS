using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Dal.Ado
{
    public class TtsVoiceDao(AppDbContext db) : ITtsVoiceDao
    {
        private readonly AppDbContext _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<IEnumerable<TtsVoice>> FindAllAsync(string? locale = null, Guid? modelId = null)
        {
            var q = _db.TtsVoices.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(locale))
                q = q.Where(v => v.Locale == locale ||
                                 v.Locale.StartsWith(locale + "-", StringComparison.OrdinalIgnoreCase));

            if (modelId is not null)
            {
                q = q.Where(v => v.ModelId == modelId);
            }

            return await q
                .OrderBy(v => v.ModelId)
                .ThenBy(v => v.Locale)
                .ThenBy(v => v.DisplayName)
                .ToListAsync();
        }

        public async Task<TtsVoice?> FindByIdAsync(Guid id)
        {
            return await _db.TtsVoices
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VoiceId == id);
        }

        public async Task<TtsVoice?> FindDefaultForModelAsync(Guid modelId)
        {
            return await _db.TtsVoices
                .AsNoTracking()
                .Where(v => v.ModelId == modelId)
                .OrderBy(v => v.DisplayName) 
                .FirstOrDefaultAsync();
        }

    }
}
