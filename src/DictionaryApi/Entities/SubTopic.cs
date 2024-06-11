namespace DictionaryApi.Entities;

/// <summary>
/// ქვეთემატიკა
/// </summary>
public class SubTopic
{
    /// <summary>
    /// ქვეთემატიკის იდენტიფიკატორი
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// ქვეთემატიკა ქართულად
    /// </summary>
    public string GeorgianName { get; set; }
    
    /// <summary>
    /// ქვეთემატიკა ინგლისურად
    /// </summary>
    public string EnglishName { get; set; }
    
    /// <summary>
    /// თემატიკა
    /// </summary>
    public Topic Topic { get; set; }
}