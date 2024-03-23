using DictionaryApi.Attributes;
using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class AddTopicRequest
{
    public string this[Language language] =>
        NameTranslations.First(translation => translation.Language == language).Value;
    
    [RequiredBothLanguage]
    public IEnumerable<TranslationModel> NameTranslations { get; set; }
}