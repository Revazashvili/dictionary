namespace DictionaryApi.Entities;

public class Entry
{
    public int Id { get; set; }
    
    /// <summary>
    /// მეთაური სიტყვა ორივე ენაზე
    /// </summary>
    public IEnumerable<Translation> HeadwordTranslations { get; set; }
    
    /// <summary>
    /// მეტყველების ნაწილი
    /// </summary>
    public string FunctionalLabel { get; set; }
    
    /// <summary>
    /// სტილისტიკური კვალიფიკაცია
    /// </summary>
    public string StylisticQualification { get; set; }
    
    /// <summary>
    /// მეთაური სიტყვის განმარტება ორივე ენაზე
    /// </summary>
    public IEnumerable<Translation> DefinitionTranslations { get; set; }
    
    /// <summary>
    /// საილუსტრაციო წინადადება ორივე ენაზე
    /// </summary>
    public IEnumerable<Translation> IllustrationSentenceTranslations { get; set; }
    
    /// <summary>
    /// კონტექსტის წყარო
    /// </summary>
    public string Source { get; set; }
    
    /// <summary>
    /// იდიომი
    /// </summary>
    public string Idiom { get; set; }
    
    /// <summary>
    /// სინონიმი
    /// </summary>
    public string Synonym { get; set; }
    
    /// <summary>
    /// დამატებითი კომენტარი
    /// </summary>
    public string UsageNote { get; set; }
    
    /// <summary>
    /// ფოტოს მისამართი
    /// </summary>
    public string ImageUrl { get; set; }
    
    /// <summary>
    /// თემატიკა
    /// </summary>
    public Topic Topic { get; set; }
    
    /// <summary>
    /// ქვეთემატიკა
    /// </summary>
    public SubTopic SubTopic { get; set; }
}