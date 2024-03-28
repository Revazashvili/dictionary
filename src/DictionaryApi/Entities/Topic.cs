namespace DictionaryApi.Entities;

public class Topic
{
    // Used by EF Core
    private Topic() {}

    public Topic(int id, Guid translationId)
    {
        if (translationId == Guid.Empty)
            throw new ArgumentException($"translationId is guid empty", nameof(translationId));
        
        Id = id;
        TranslationId = translationId;
    }
    
    public Topic(Guid translationId)
    {
        if (translationId == Guid.Empty)
            throw new ArgumentException($"translationId is guid empty", nameof(translationId));
        
        TranslationId = translationId;
    }
    
    public int Id { get; private set; }
    public Guid TranslationId { get; private set; }
    public IEnumerable<SubTopic> SubTopics { get; private set; }
}