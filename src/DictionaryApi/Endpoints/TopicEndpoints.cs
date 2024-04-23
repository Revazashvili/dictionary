using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using DictionaryApi.EndpointFilters;
using DictionaryApi.Models;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

public static class TopicEndpoints
{
    public static void MapTopicApi(this IEndpointRouteBuilder topicEndpointRouteBuilder)
    {
        topicEndpointRouteBuilder.MapGet("topic",
            (ITopicService topicService, CancellationToken cancellationToken) =>
                topicService.GetAllAsync(cancellationToken));

        topicEndpointRouteBuilder.MapGet("topic/{id:int}",
            async ([Required] int id, ITopicService topicService, CancellationToken cancellationToken) => 
            id == 0 ? Results.BadRequest("id is not valid")
                : Results.Ok(await topicService.GetByIdAsync(id, cancellationToken)));

        topicEndpointRouteBuilder.MapPost("topic", async ([FromBody] AddTopicRequest addTopicRequest,
                ITopicService topicService, CancellationToken cancellationToken) =>
            {
                addTopicRequest.Validate();
                var topicId = await topicService.AddAsync(addTopicRequest, cancellationToken);

                return Results.Ok(topicId);
            })
            .Accepts<AddTopicRequest>(MediaTypeNames.Application.Json)
            .Produces<int>()
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();

        topicEndpointRouteBuilder.MapPut("topic", async ([FromBody] UpdateTopicRequest updateTopicRequest,
                ITopicService topicService, CancellationToken cancellationToken) =>
            {
                updateTopicRequest.Validate();
                await topicService.UpdateAsync(updateTopicRequest, cancellationToken);

                return Results.Ok();
            })
            .Accepts<UpdateTopicRequest>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();

        topicEndpointRouteBuilder.MapDelete("topic/{id:int}",
                async (int id, ITopicService topicService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await topicService.DeleteAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();
    }
}