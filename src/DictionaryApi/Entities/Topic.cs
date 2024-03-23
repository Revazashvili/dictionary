namespace DictionaryApi.Entities;

public class Topic
{
    public Topic(Guid id, IEnumerable<Translation> nameTranslations)
    {
        if (!nameTranslations.Any())
            throw new ArgumentNullException(nameof(nameTranslations));
        
        Id = id;
        NameTranslations = nameTranslations;
    }
    
    public Guid Id { get; private set; }
    public IEnumerable<Translation> NameTranslations { get; private set; }
    public IEnumerable<SubTopic> SubTopics { get; private set; }
}