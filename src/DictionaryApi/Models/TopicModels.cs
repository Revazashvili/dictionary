using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class AddTopicRequest
{
    public List<Translation> NameTranslations { get; set; }
}

public class UpdateTopicRequest
{
    public int Id { get; set; }
    public List<Translation> NameTranslations { get; set; }
}