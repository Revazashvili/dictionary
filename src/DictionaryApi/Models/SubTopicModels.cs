namespace DictionaryApi.Models;

public class AddSubTopicRequest : Translatable
{
    public int TopicId { get; set; }
    
    public override void Validate()
    {
        if (TopicId == 0)
            throw new ArgumentNullException(nameof(TopicId), "topic id is not valid");
        
        base.Validate();
    }
}

public class UpdateSubTopicRequest : Translatable
{
    public int Id { get; set; }
    
    public override void Validate()
    {
        if (Id == 0)
            throw new ArgumentNullException(nameof(Id), "id is not valid");
        
        base.Validate();
    }
}