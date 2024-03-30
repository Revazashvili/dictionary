namespace DictionaryApi.Entities;

/// <summary>
/// Object for holding translation
/// </summary>
public class Translation
{ 
    // Used by EF Core
    private Translation() { }
    
    public Translation(Language language, string value)
    {
        Language = language;
        Value = value;
    }
    public Language Language { get; private set; }
    public string Value { get; private set; }
}