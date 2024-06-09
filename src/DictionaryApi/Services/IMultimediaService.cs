namespace DictionaryApi.Services;

public interface IMultimediaService
{
    Task<string> UploadAsync(IFormFile file);
    Task<byte[]> GetAsync(string fileName);
    Task RemoveAsync(string fileName);
}