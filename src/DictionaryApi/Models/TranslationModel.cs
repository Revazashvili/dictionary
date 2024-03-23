using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class TranslationModel
{
    public Language Language { get; set; }
    public string Value { get; set; }
}