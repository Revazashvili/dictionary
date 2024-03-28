using DictionaryApi.Entities;
using DictionaryApi.Models;

namespace DictionaryApi.Extensions;

internal static class TranslationExtensions
{
    private static Translation ToTranslation(this TranslationModel translationModel, Guid translationId) => 
        new(translationId,translationModel.Language, translationModel.Value);

    internal static IEnumerable<Translation> ToTranslations(this IEnumerable<TranslationModel> translationModels,
        Guid translationId) => translationModels.Select(model => model.ToTranslation(translationId));
}