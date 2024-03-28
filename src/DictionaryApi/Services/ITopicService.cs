using DictionaryApi.Models;

namespace DictionaryApi.Services;

public interface ITopicService
{
    Task<IEnumerable<TopicDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TopicDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(AddTopicRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateTopicRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteTopicRequest request, CancellationToken cancellationToken);
}