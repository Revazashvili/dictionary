using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class SubTopicDto
{
    public int Id { get; set; }
    public IEnumerable<Translation> NameTranslations { get; set; }
}