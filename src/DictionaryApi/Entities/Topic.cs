namespace DictionaryApi.Entities;

/// <summary>
/// თემატიკა
/// </summary>
public class Topic
{
    /// <summary>
    /// თემატიკის იდენტიფიკატორი
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// თემატიკა ქართულად
    /// </summary>
    public string GeorgianName { get; set; }
    
    /// <summary>
    /// თემატიკა ინგლისურად
    /// </summary>
    public string EnglishName { get; set; }
    
    /// <summary>
    /// ქვეთემატიკები
    /// </summary>
    public ICollection<SubTopic> SubTopics { get; set; }
}