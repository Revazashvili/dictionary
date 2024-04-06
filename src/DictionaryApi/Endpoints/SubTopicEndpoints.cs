using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using DictionaryApi.Models;
using DictionaryApi.Services;
using DictionaryApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

public static class SubTopicEndpoints
{
    public static void MapSubTopicApi(this IEndpointRouteBuilder subTopicEndpointRouteBuilder)
    {
        subTopicEndpointRouteBuilder.MapPost("topic/subTopic", async ([FromBody] AddSubTopicRequest addSubTopicRequest,
                ITopicService topicService, CancellationToken cancellationToken) =>
            {
                if (addSubTopicRequest.TopicId == 0)
                    return Results.BadRequest("id is not valid");
                
                var errorMessage = addSubTopicRequest.Validate();
                if (!string.IsNullOrEmpty(errorMessage))
                    return Results.BadRequest(errorMessage);
                
                var topicId = await topicService.AddSubTopicAsync(addSubTopicRequest, cancellationToken);

                return Results.Ok(topicId);
            })
            .Accepts<AddSubTopicRequest>(MediaTypeNames.Application.Json)
            .Produces<int>();

        subTopicEndpointRouteBuilder.MapPut("topic/subTopic", async ([FromBody] [Required] UpdateSubTopicRequest updateSubTopicRequest,
                ITopicService topicService, CancellationToken cancellationToken) =>
            {
                if (updateSubTopicRequest.Id == 0)
                    return Results.BadRequest("sub topic id is not valid");
                
                var errorMessage = updateSubTopicRequest.Validate();
                if (!string.IsNullOrEmpty(errorMessage))
                    return Results.BadRequest(errorMessage);
                
                await topicService.UpdateSubTopicAsync(updateSubTopicRequest, cancellationToken);

                return Results.Ok();
            })
            .Accepts<UpdateSubTopicRequest>(MediaTypeNames.Application.Json);
        
        subTopicEndpointRouteBuilder.MapDelete("topic/subTopic/{id:int}",
                async (int id, ITopicService topicService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await topicService.DeleteSubTopicAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json);
    }
}