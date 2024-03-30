using DictionaryApi.Entities;
using DictionaryApi.Models;

namespace DictionaryApi.Services;

public interface ITopicService
{
    Task<IEnumerable<Topic>> GetAllAsync(CancellationToken cancellationToken);
    Task<Topic> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(AddTopicRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateTopicRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}