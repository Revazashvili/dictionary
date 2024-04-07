namespace DictionaryApi.Models;

public class AddTopicRequest : Translatable;

public class UpdateTopicRequest : Translatable
{
    public int Id { get; set; }

    public override void Validate()
    {
        if (Id == 0)
            throw new ArgumentNullException(nameof(Id), "id is not valid");
        
        base.Validate();
    }
}