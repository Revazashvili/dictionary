namespace DictionaryApi.Entities;

public class SubTopic
{
    public int Id { get; set; }
    public ICollection<Translation> NameTranslations { get; set; }
    public Topic Topic { get; set; }
}