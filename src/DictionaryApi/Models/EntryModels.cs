namespace DictionaryApi.Models;

public class BaseEntryRequest
{
    /// <summary>
    /// მეთაური სიტყვა ქართულად
    /// </summary>
    public string GeorgianHeadword { get; set; }
    
    /// <summary>
    /// მეთაური სიტყვა ინგლისურად
    /// </summary>
    public string EnglishHeadword { get; set; }
    
    /// <summary>
    /// მეტყველების ნაწილი
    /// </summary>
    public string FunctionalLabel { get; set; }
    
    /// <summary>
    /// სტილისტიკური კვალიფიკაცია
    /// </summary>
    public string StylisticQualification { get; set; }
    
    /// <summary>
    /// მეთაური სიტყვის განმარტება ქართულად
    /// </summary>
    public string GeorgianDefinition { get; set; }
    
    /// <summary>
    /// მეთაური სიტყვის განმარტება ინგლისურად
    /// </summary>
    public string EnglishDefinition { get; set; }
    
    /// <summary>
    /// საილუსტრაციო წინადადება ქართულად
    /// </summary>
    public string GeorgianIllustrationSentence { get; set; }
    
    /// <summary>
    /// საილუსტრაციო წინადადება ინგლისურად
    /// </summary>
    public string EnglishIllustrationSentence { get; set; }
    
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

    public virtual void Validate()
    {
        if (string.IsNullOrEmpty(GeorgianHeadword))
            throw new ArgumentNullException(nameof(GeorgianHeadword), "georgian headword is not valid");
        
        if (string.IsNullOrEmpty(EnglishHeadword))
            throw new ArgumentNullException(nameof(EnglishHeadword), "english headword is not valid");

        if (string.IsNullOrEmpty(FunctionalLabel))
            throw new ArgumentNullException(nameof(FunctionalLabel), "functional label is not valid");

        if (string.IsNullOrEmpty(StylisticQualification))
            throw new ArgumentNullException(nameof(StylisticQualification), "stylistic qualification is not valid");

        if (string.IsNullOrEmpty(GeorgianDefinition))
            throw new ArgumentNullException(nameof(GeorgianDefinition), "georgian definition is not valid");

        if (string.IsNullOrEmpty(EnglishDefinition))
            throw new ArgumentNullException(nameof(EnglishDefinition), "english definition is not valid");
        
        if (string.IsNullOrEmpty(GeorgianIllustrationSentence))
            throw new ArgumentNullException(nameof(GeorgianIllustrationSentence), "georgian illustration sentence is not valid");

        if (string.IsNullOrEmpty(GeorgianIllustrationSentence))
            throw new ArgumentNullException(nameof(GeorgianIllustrationSentence), "english illustration sentence is not valid");

    }
}

public class AddEntryRequest : BaseEntryRequest;

public class UpdateEntryRequest : BaseEntryRequest
{
    /// <summary>
    /// იდენტიფიკატორი
    /// </summary>
    public int Id { get; set; }

    public override void Validate()
    {
        if (Id == 0)
            throw new ArgumentNullException(nameof(Id), "id is not valid");
        
        base.Validate();
    }
}