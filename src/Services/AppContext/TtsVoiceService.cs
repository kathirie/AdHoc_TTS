using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Models.AppContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.EntityFrameworkCore;


namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public class TtsVoiceService : ITtsVoiceService
{
    private readonly AppDbContext _db;
    public TtsVoiceService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<TtsVoice>> GetAllAsync(string? locale = null, string? provider = null)
    {
        var q = _db.TtsVoices.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(locale))
            q = q.Where(v => v.Locale == locale || v.Locale.StartsWith(locale + "-", StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(provider))
            q = q.Where(v => v.Provider == provider);

        return await q.OrderBy(v => v.Provider).ThenBy(v => v.Locale).ThenBy(v => v.DisplayName).ToListAsync();
    }

    public async Task<TtsVoice?> GetByIdAsync(Guid id)
        => await _db.TtsVoices.AsNoTracking().FirstOrDefaultAsync(v => v.VoiceId == id);
}
