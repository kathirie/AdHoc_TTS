using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Dal.Interface;

public interface ITargetTextDao
{
    Task<IEnumerable<TargetText>> FindAllAsync();
    Task<IEnumerable<string>> FindAllFrontTextsAsync();
}
