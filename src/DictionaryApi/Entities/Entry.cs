namespace DictionaryApi.Entities;

public class Entry
{
    public int Id { get; set; }
    public EntityStatus Status { get; set; }
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
    public Topic Topic { get; set; }
    public SubTopic SubTopic { get; set; }
}