namespace DictionaryApi.Models;

public class AddTopicRequest : Translatable;

public class UpdateTopicRequest : Translatable
{
    public int Id { get; set; }
}