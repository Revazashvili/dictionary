namespace DictionaryApi.Entities;

public class Topic
{
    public int Id { get; set; }
    public EntityStatus Status { get; set; }
    public string GeorgianName { get; set; }
    public string EnglishName { get; set; }
    public ICollection<SubTopic> SubTopics { get; set; }
}