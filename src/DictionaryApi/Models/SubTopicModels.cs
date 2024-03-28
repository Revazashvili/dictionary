namespace DictionaryApi.Models;

public class SubTopicDto
{
    public int Id { get; set; }
    public IEnumerable<TranslationModel> NameTranslations { get; set; }
    public TopicDto Topic { get; set; }
}