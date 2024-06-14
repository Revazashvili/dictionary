namespace DictionaryApi.Entities;

public class Multimedia
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public byte[] Blob { get; set; }
    public string ContentType { get; set; }
}