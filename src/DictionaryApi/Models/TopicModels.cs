using System.ComponentModel.DataAnnotations;
using DictionaryApi.Attributes;

namespace DictionaryApi.Models;

public class TopicDto
{
    public int Id { get; set; }
    public IEnumerable<TranslationModel> NameTranslations { get; set; }
    public IEnumerable<SubTopicDto> SubTopics { get; set; }
}

public class AddTopicRequest
{
    [Required]
    [RequiredBothLanguage]
    public IEnumerable<TranslationModel> NameTranslations { get; set; }
}

public class UpdateTopicRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [RequiredBothLanguage]
    public IEnumerable<TranslationModel> NameTranslations { get; set; }
}

public class DeleteTopicRequest
{
    public int Id { get; set; }
}