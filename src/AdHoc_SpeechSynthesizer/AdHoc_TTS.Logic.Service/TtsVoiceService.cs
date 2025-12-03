using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;


namespace AdHoc_SpeechSynthesizer.Services;

public class TtsVoiceService(ITtsVoiceDao dao) : ITtsVoiceService
{
    private readonly ITtsVoiceDao _dao = dao ?? throw new ArgumentNullException(nameof(dao));

    public Task<IEnumerable<TtsVoice>> GetAllAsync(string? locale = null, Guid? modelId = null)
        => _dao.FindAllAsync(locale, modelId);

    public Task<TtsVoice?> GetByIdAsync(Guid id)
        => _dao.FindByIdAsync(id);
}
