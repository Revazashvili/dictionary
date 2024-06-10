namespace DictionaryApi.Services;

public class MultimediaService : IMultimediaService
{
    private const string FolderPath = "../images";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MultimediaService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid().ToString()}{file.FileName}";
        
        var filePath = GetFilePath(fileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return GetImageUrl(fileName);
    }

    public Task<byte[]> GetAsync(string fileName)
    {
        var filePath = GetFilePath(fileName);

        return File.ReadAllBytesAsync(filePath);
    }

    public Task RemoveAsync(string fileName)
    {
        var filePath = GetFilePath(fileName);
        
        File.Delete(filePath);
        
        return Task.CompletedTask;
    }
    
    private string GetFilePath(string imageName)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        var filePath = Path.Combine(FolderPath, $"{imageName}");
        return filePath;
    }

    private string GetImageUrl(string imageName)
    {
        var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        return $"{baseUrl}/api/multimedia/{imageName}";
    }
}