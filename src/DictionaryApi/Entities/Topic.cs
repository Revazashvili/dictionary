namespace DictionaryApi.Entities;

public class Topic
{
    public int Id { get; set; }
    public ICollection<Translation> NameTranslations { get; set; }
    public ICollection<SubTopic> SubTopics { get; set; }
}