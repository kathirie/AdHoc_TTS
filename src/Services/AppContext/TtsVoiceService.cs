using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;


namespace AdHoc_SpeechSynthesizer.Services.AppContext;

public class TtsVoiceService : ITtsVoiceService
{
    private readonly ITtsVoiceDao _dao;

    public TtsVoiceService(ITtsVoiceDao dao)
    {
        _dao = dao;
    }

    public Task<IEnumerable<TtsVoice>> GetAllAsync(string? locale = null, string? provider = null)
        => _dao.FindAllAsync(locale, provider);

    public Task<TtsVoice?> GetByIdAsync(Guid id)
        => _dao.FindByIdAsync(id);
}
