using DictionaryApi.Entities;

namespace DictionaryApi.Services;

public interface IMultimediaService
{
    Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken);
    Task<Multimedia> GetAsync(string fileName, CancellationToken cancellationToken);
    Task RemoveAsync(string imageUrl, CancellationToken cancellationToken);
}