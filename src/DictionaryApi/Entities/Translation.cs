namespace DictionaryApi.Entities;

/// <summary>
/// Object for holding translation
/// </summary>
public class Translation
{
    public Language Language { get; set; }
    public string Value { get; set; }
}