using DictionaryApi.Entities;
using DictionaryApi.Models;

namespace DictionaryApi.Extensions;

internal static class TranslationExtensions
{
    private static Translation ToTranslation(this TranslationModel translationModel) => 
        new(translationModel.Language, translationModel.Value);

    internal static IEnumerable<Translation> ToTranslations(this IEnumerable<TranslationModel> translationModels) =>
        translationModels.Select(model => model.ToTranslation());
}