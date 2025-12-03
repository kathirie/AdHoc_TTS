using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;

namespace AdHoc_SpeechSynthesizer.Services;

public class TtsModelService : ITtsModelService
{
    private readonly ITtsModelDao _dao;

    public TtsModelService(ITtsModelDao dao)
    {
        _dao = dao;
    }

    public async Task<IEnumerable<TtsModel>> GetAllAsync()
        => await _dao.FindAllAsync();

    public async Task<TtsModel?> GetByIdAsync(Guid id)
        => await _dao.FindByIdAsync(id);
}
