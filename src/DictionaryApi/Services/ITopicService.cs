using DictionaryApi.Entities;
using DictionaryApi.Models;

namespace DictionaryApi.Services;

public interface ITopicService
{
    Task<IEnumerable<Topic>> GetAllAsync(CancellationToken cancellationToken);
    Task<Topic> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(AddTopicRequest request, CancellationToken cancellationToken);
    Task<int> AddSubTopicAsync(AddSubTopicRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateTopicRequest request, CancellationToken cancellationToken);
    Task UpdateSubTopicAsync(UpdateSubTopicRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task DeleteSubTopicAsync(int id, CancellationToken cancellationToken);
    Task<SubTopic> GetSubTopicAsync(int subTopicId, CancellationToken cancellationToken);
    Task ActivateTopicAsync(int id, CancellationToken cancellationToken);
    Task DeactivateTopicAsync(int id, CancellationToken cancellationToken);
    Task ActivateSubTopicAsync(int id, CancellationToken cancellationToken);
    Task DeactivateSubTopicAsync(int id, CancellationToken cancellationToken);
}