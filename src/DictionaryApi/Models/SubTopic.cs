namespace DictionaryApi.Models;

public class SubTopic
{
    public SubTopic(int id, IEnumerable<Translation> nameTranslations)
    {
        if (!nameTranslations.Any())
            throw new ArgumentNullException(nameof(nameTranslations));
        
        Id = id;
        NameTranslations = nameTranslations;
    }

    public int Id { get; private set; }
    public IEnumerable<Translation> NameTranslations { get; private set; }
    public Topic Topic { get; private set; }
}