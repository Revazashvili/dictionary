namespace DictionaryApi.Entities;

public class SubTopic : Entity
{
    public SubTopic(Guid id, string name) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; set; }
    public Topic Topic { get; set; }
}