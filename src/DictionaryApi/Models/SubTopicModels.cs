namespace DictionaryApi.Models;

public class AddSubTopicRequest : Translatable
{
    public int TopicId { get; set; }
}

public class UpdateSubTopicRequest : Translatable
{
    public int Id { get; set; }
}