using AdHoc_SpeechSynthesizer.Domain;

namespace AdHoc_SpeechSynthesizer.Dal.Interface;

public interface IMessageTemplateDao
{
    Task<IEnumerable<MessageTemplate>> FindAllAsync();
    Task<MessageTemplate?> FindByIdAsync(Guid id);
}
