using DictionaryApi.Entities;

namespace DictionaryApi.Validators;

internal static class TranslationsValidator
{
    internal static string Validate(this IEnumerable<Translation> translations)
    {
        if (translations.Count() != 2)
            return "translations must contain both language";

        var georgian = translations.FirstOrDefault(translation => translation.Language == Language.Ka);
        if (georgian is null || string.IsNullOrEmpty(georgian.Value))
            return "translations does not contain georgian value";

        var english = translations.FirstOrDefault(translation => translation.Language == Language.En);
        if (english is null || string.IsNullOrEmpty(english.Value))
            return "translations does not contain georgian value";

        return string.Empty;
    }
}