using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using DictionaryApi.EndpointFilters;
using DictionaryApi.Models;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

public static class SubTopicEndpoints
{
    public static void MapSubTopicApi(this IEndpointRouteBuilder subTopicEndpointRouteBuilder)
    {
        subTopicEndpointRouteBuilder.MapPost("topic/subTopic", async ([FromBody] AddSubTopicRequest addSubTopicRequest,
                ITopicService topicService, CancellationToken cancellationToken) =>
            {
                addSubTopicRequest.Validate();
                var topicId = await topicService.AddSubTopicAsync(addSubTopicRequest, cancellationToken);

                return Results.Ok(topicId);
            })
            .Accepts<AddSubTopicRequest>(MediaTypeNames.Application.Json)
            .Produces<int>()
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();

        subTopicEndpointRouteBuilder.MapPut("topic/subTopic", async ([FromBody] [Required] UpdateSubTopicRequest updateSubTopicRequest,
                ITopicService topicService, CancellationToken cancellationToken) =>
            { 
                updateSubTopicRequest.Validate();
                await topicService.UpdateSubTopicAsync(updateSubTopicRequest, cancellationToken);

                return Results.Ok();
            })
            .Accepts<UpdateSubTopicRequest>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();
        
        subTopicEndpointRouteBuilder.MapDelete("topic/subTopic/{id:int}",
                async (int id, ITopicService topicService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await topicService.DeleteSubTopicAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<SuperAdminPrivilegesEndpointFilter>();
        
        subTopicEndpointRouteBuilder.MapPut("topic/subTopic/activate/{id:int}",
                async (int id, ITopicService topicService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await topicService.ActivateSubTopicAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<SuperAdminPrivilegesEndpointFilter>();
        
        subTopicEndpointRouteBuilder.MapPut("topic/subTopic/deactivate/{id:int}",
                async (int id, ITopicService topicService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await topicService.DeactivateSubTopicAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<SuperAdminPrivilegesEndpointFilter>();
    }
}