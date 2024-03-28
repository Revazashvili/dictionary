namespace DictionaryApi.Entities;

/// <summary>
/// Object for holding translation
/// </summary>
public class Translation
{ 
    // Used by EF Core
    private Translation() { }
    
    public Translation(Guid translationId,Language language, string value)
    {
        if (translationId == Guid.Empty)
            throw new ArgumentException($"translationId is guid empty", nameof(translationId));

        TranslationId = translationId;
        Language = language;
        Value = value;
    }

    public int Id { get; private set; }
    public Guid TranslationId { get; set; }
    public Language Language { get; private set; }
    public string Value { get; private set; }
}