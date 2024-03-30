namespace DictionaryApi.Entities;

public class SubTopic
{
    // Used by EF Core
    private SubTopic() {}
    
    public SubTopic(int id, Guid translationId)
    {
        if (translationId == Guid.Empty)
            throw new ArgumentException($"translationId is guid empty", nameof(translationId));
        
        Id = id;
        // TranslationId = translationId;
    }

    public int Id { get; private set; }
    // public Guid TranslationId { get; private set; }
    
    public IEnumerable<Translation> NameTranslations { get; set; }
    public Topic Topic { get; private set; }
}