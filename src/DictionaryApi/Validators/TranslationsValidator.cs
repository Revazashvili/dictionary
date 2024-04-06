using DictionaryApi.Models;

namespace DictionaryApi.Validators;

internal static class TranslationsValidator
{
    internal static string Validate(this Translatable translatable)
    {
        if (string.IsNullOrEmpty(translatable.GeorgianName))
            return "georgian translation is empty";

        if (string.IsNullOrEmpty(translatable.EnglishName))
            return "english translation is empty";

        return string.Empty;
    }
}