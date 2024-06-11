namespace DictionaryApi.Entities;

public class SubTopic
{
    public int Id { get; set; }
    public EntityStatus Status { get; set; }
    public string GeorgianName { get; set; }
    public string EnglishName { get; set; }
    public Topic Topic { get; set; }
}