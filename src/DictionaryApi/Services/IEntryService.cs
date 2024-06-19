using DictionaryApi.Entities;
using DictionaryApi.Models;

namespace DictionaryApi.Services;

internal interface IEntryService
{
    Task<IEnumerable<Entry>> GetAllAsync(EntryFilterModelWithPagination filter, CancellationToken cancellationToken);
    Task<int> GetCountAsync(BaseEntryFilterModel filter, CancellationToken cancellationToken);
    Task<Entry> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> AddAsync(AddEntryRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateEntryRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task ActivateAsync(int id, CancellationToken cancellationToken);
    Task DeactivateAsync(int id, CancellationToken cancellationToken);
}