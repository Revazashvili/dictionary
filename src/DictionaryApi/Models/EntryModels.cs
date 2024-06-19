using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class BaseEntryRequest
{
    public string GeorgianHeadword { get; set; }
    public string EnglishHeadword { get; set; }
    public string FunctionalLabel { get; set; }
    public string StylisticQualification { get; set; }
    public string GeorgianDefinition { get; set; }
    public string EnglishDefinition { get; set; }
    public string GeorgianIllustrationSentence { get; set; }
    public string EnglishIllustrationSentence { get; set; }
    public string Source { get; set; }
    public string Idiom { get; set; }
    public string Synonym { get; set; }
    public string UsageNote { get; set; }
    public string ImageUrl { get; set; }
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
    public int Id { get; set; }

    public override void Validate()
    {
        if (Id == 0)
            throw new ArgumentNullException(nameof(Id), "id is not valid");
        
        base.Validate();
    }
}

public class BaseEntryFilterModel
{
    public BaseEntryFilterModel(string searchText, int? subTopicId, EntityStatus? status)
    {
        SearchText = searchText;
        SubTopicId = subTopicId;
        Status = status;
    }

    public string SearchText { get; set; }
    public int? SubTopicId { get; set; }
    public EntityStatus? Status { get; set; }
}

public class EntryFilterModelWithPagination : BaseEntryFilterModel
{
    public EntryFilterModelWithPagination(int pageNumber, int pageSize, string searchText, int? subTopicId, EntityStatus? status)
        : base(searchText, subTopicId, status)
    {
        Pagination = new Pagination(pageNumber, pageSize);
    }

    public Pagination Pagination { get; set; }
}