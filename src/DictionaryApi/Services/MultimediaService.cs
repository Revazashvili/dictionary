using DictionaryApi.Entities;
using DictionaryApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Services;

public class MultimediaService : IMultimediaService
{
    private readonly DictionaryDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MultimediaService(DictionaryDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid().ToString();
        await using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream, cancellationToken);

            var multimedia = new Multimedia
            {
                Blob = stream.ToArray(),
                ContentType = file.ContentType,
                FileName = fileName
            };

            await _context.Multimedia.AddAsync(multimedia, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        return GetImageUrl(fileName);
    }

    public async Task<Multimedia> GetAsync(string fileName, CancellationToken cancellationToken)
    {
        var multimedia = await _context.Multimedia.FirstOrDefaultAsync(m => m.FileName == fileName, cancellationToken);

        return multimedia;
    }

    public async Task RemoveAsync(string imageUrl, CancellationToken cancellationToken)
    {
        var fileName = imageUrl.Split('/').Last();
        
        var multimedia = await _context.Multimedia.FirstOrDefaultAsync(m => m.FileName == fileName, cancellationToken);

        _context.Multimedia.Remove(multimedia);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private string GetImageUrl(string imageName)
    {
        var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        return $"{baseUrl}/api/multimedia/{imageName}";
    }
}