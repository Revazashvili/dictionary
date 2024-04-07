namespace DictionaryApi.Models;

public class Translatable
{
    public string GeorgianName { get; set; }
    public string EnglishName { get; set; }

    public virtual void Validate()
    {
        if (string.IsNullOrEmpty(GeorgianName))
            throw new ArgumentNullException(nameof(GeorgianName),"georgian translation is empty");

        if (string.IsNullOrEmpty(EnglishName))
            throw new ArgumentNullException(nameof(EnglishName),"english translation is empty");
    }
}