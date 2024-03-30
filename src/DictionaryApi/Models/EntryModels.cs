using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class BaseEntryRequest
{
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
    /// ქვეკატეგორიის იდენტიფიკატორი
    /// </summary>
    public int SubTopicId { get; set; }
}

public class AddEntryRequest : BaseEntryRequest;

public class UpdateEntryRequest : BaseEntryRequest
{
    /// <summary>
    /// იდენტიფიკატორი
    /// </summary>
    public int Id { get; set; }
}