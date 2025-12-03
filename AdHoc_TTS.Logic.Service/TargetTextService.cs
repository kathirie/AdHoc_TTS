using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces;

namespace AdHoc_SpeechSynthesizer.Services;

public class TargetTextService : ITargetTextService
{
    private readonly ITargetTextDao _dao;

    public TargetTextService(ITargetTextDao dao)
    {
        _dao = dao;
    }

    public Task<IEnumerable<TargetText>> GetAllAsync()
        => _dao.FindAllAsync();

    public Task<IEnumerable<string>> GetAllFrontTextsAsync()
        => _dao.FindAllFrontTextsAsync();
}
