using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Domain;
using AdHoc_SpeechSynthesizer.Services.Interfaces;

namespace AdHoc_SpeechSynthesizer.Services;

public class TargetTextService(ITargetTextDao dao) : ITargetTextService
{
    private readonly ITargetTextDao _dao = dao ?? throw new ArgumentNullException(nameof(dao));

    public Task<IEnumerable<TargetText>> GetAllAsync()
        => _dao.FindAllAsync();

    public Task<IEnumerable<string>> GetAllFrontTextsAsync()
        => _dao.FindAllFrontTextsAsync();
}
