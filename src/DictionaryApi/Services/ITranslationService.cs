using DictionaryApi.Models;

namespace DictionaryApi.Services;

public interface ITranslationService
{
    Task<IEnumerable<TranslationModel>> GetByTranslationIdAsync(Guid translationId, CancellationToken cancellationToken);
    Task AddAsync(Guid translationId, IEnumerable<TranslationModel> translationModels, CancellationToken cancellationToken);
    Task<bool> ExistsWithNamesAsync(IEnumerable<TranslationModel> translationModels, CancellationToken cancellationToken);
    Task DeleteByTranslationIdAsync(Guid translationId, CancellationToken cancellationToken);
}