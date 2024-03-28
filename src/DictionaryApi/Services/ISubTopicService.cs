using DictionaryApi.Models;

namespace DictionaryApi.Services;

public interface ISubTopicService
{
    Task<IEnumerable<SubTopicDto>> GetByTopicIdAsync(int topicId, CancellationToken cancellationToken);
}