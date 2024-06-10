using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using DictionaryApi.EndpointFilters;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

public static class MultimediaEndpoints
{
    public static void MapMultimediaApi(this IEndpointRouteBuilder multimediaEndpointRouteBuilder)
    {
        multimediaEndpointRouteBuilder.MapGet("multimedia/{fileName}",
            async ([Required] string fileName, IMultimediaService multimediaService) =>
            {
                if (string.IsNullOrEmpty(fileName))
                    return Results.BadRequest("file name is not valid");

                var file = await multimediaService.GetAsync(fileName);
                return Results.File(file, MediaTypeNames.Image.Jpeg);
            })
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();

        multimediaEndpointRouteBuilder.MapPost("multimedia", async ([FromForm] IFormFile file, 
                IMultimediaService multimediaService) =>
            {
                if (file is null)
                    throw new ArgumentNullException(nameof(file));

                var url = await multimediaService.UploadAsync(file);

                return Results.Ok(url);
            })
            .Accepts<IFormFile>(MediaTypeNames.Image.Jpeg)
            .Produces<string>()
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();
    }
}