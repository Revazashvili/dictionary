namespace DictionaryApi.Entities;

public class Topic : Entity
{
    public Topic(Guid id, string name) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; set; }
}