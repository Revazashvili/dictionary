namespace DictionaryApi.Models;

public class Topic
{
    public Topic(int id, IEnumerable<Translation> nameTranslations)
    {
        if (!nameTranslations.Any())
            throw new ArgumentNullException(nameof(nameTranslations));
        
        Id = id;
        NameTranslations = nameTranslations;
    }
    
    public int Id { get; private set; }
    public IEnumerable<Translation> NameTranslations { get; private set; }
    public IEnumerable<SubTopic> SubTopics { get; private set; }
}