using DictionaryApi.Extensions;
using DictionaryApi.Models;
using DictionaryApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Services;

public class TranslationService : ITranslationService
{
    private readonly DictionaryDbContext _context;

    public TranslationService(DictionaryDbContext context) => _context = context;

    public async Task<IEnumerable<TranslationModel>> GetByTranslationIdAsync(Guid translationId, CancellationToken cancellationToken)
    {
        // var translations = await _context.Translations
        //     .Where(translation => translation.TranslationId == translationId)
        //     .Select(translation => new TranslationModel
        //     {
        //         Language = translation.Language,
        //         Value = translation.Value
        //     }).ToListAsync(cancellationToken);

        return null;
    }

    public async Task AddAsync(Guid translationId, IEnumerable<TranslationModel> translationModels, CancellationToken cancellationToken)
    {
        // var translations = translationModels.ToTranslations(translationId);
        // await _context.Translations.AddRangeAsync(translations, cancellationToken);
        //
        // await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithNamesAsync(IEnumerable<TranslationModel> translationModels, 
        CancellationToken cancellationToken)
    {
        var values = translationModels.Select(model => model.Value).ToList();
        
        var existsTranslationsWithNames = await _context.Translations
            .AnyAsync(translation => values.Contains(translation.Value),
                cancellationToken: cancellationToken);
        
        return existsTranslationsWithNames;

    }

    public async Task DeleteByTranslationIdAsync(Guid translationId, CancellationToken cancellationToken)
    {
        // var translations = await _context.Translations
        //     .Where(translation => translation.TranslationId == translationId)
        //     .ToListAsync(cancellationToken);
        //
        // _context.Translations.RemoveRange(translations);

        await _context.SaveChangesAsync(cancellationToken);
    }
}