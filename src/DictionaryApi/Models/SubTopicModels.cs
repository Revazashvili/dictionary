using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class AddSubTopicRequest
{
    public int TopicId { get; set; }
    public List<Translation> NameTranslations { get; set; }
}

public class UpdateSubTopicRequest
{
    public int Id { get; set; }
    public List<Translation> NameTranslations { get; set; }
}