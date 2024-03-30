namespace DictionaryApi.Entities;

public class Topic
{
    // Used by EF Core
    private Topic() {}
    
    public Topic(IEnumerable<Translation> nameTranslations)
    {
        NameTranslations = nameTranslations.ToList();
    }
    
    public int Id { get; private set; }
    public ICollection<Translation> NameTranslations { get; private set; }
    public ICollection<SubTopic> SubTopics { get; private set; }
}