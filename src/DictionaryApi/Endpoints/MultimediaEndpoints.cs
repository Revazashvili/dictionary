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
        var multimediaEndpointRouteBuilderGroup = multimediaEndpointRouteBuilder
            .MapGroup("multimedia")
            .DisableAntiforgery();

        multimediaEndpointRouteBuilderGroup.MapGet("/{fileName}",
            async ([Required] string fileName, IMultimediaService multimediaService, CancellationToken cancellationToken) =>
            {
                if (string.IsNullOrEmpty(fileName))
                    return Results.BadRequest("file name is not valid");

                var multimedia = await multimediaService.GetAsync(fileName, cancellationToken);
                return Results.File(multimedia.Blob, multimedia.ContentType);
            });

        multimediaEndpointRouteBuilderGroup.MapPost("/", async ([FromForm] IFormFile file,
                IMultimediaService multimediaService, CancellationToken cancellationToken) =>
            {
                ArgumentNullException.ThrowIfNull(file);

                var url = await multimediaService.UploadAsync(file, cancellationToken);

                return Results.Ok(url);
            })
            .Accepts<IFormFile>(MediaTypeNames.Multipart.FormData)
            .Produces<string>()
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();
    }
}