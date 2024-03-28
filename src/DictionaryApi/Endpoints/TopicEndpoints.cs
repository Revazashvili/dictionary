using System.Net.Mime;
using DictionaryApi.Models;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

public static class TopicEndpoints
{
    public static IEndpointRouteBuilder MapTopicApi(this IEndpointRouteBuilder topicEndpointRouteBuilder)
    {
        topicEndpointRouteBuilder.MapGet("topic",
            (ITopicService topicService, CancellationToken cancellationToken) =>
                topicService.GetAllAsync(cancellationToken));

        topicEndpointRouteBuilder.MapGet("topic/{id:int}",
            (int id, ITopicService topicService, CancellationToken cancellationToken) =>
                topicService.GetByIdAsync(id, cancellationToken));

        topicEndpointRouteBuilder.MapPost("topic", ([FromBody] AddTopicRequest addTopicRequest,
                    ITopicService topicService, CancellationToken cancellationToken) =>
                topicService.AddAsync(addTopicRequest, cancellationToken))
            .Accepts<AddTopicRequest>(MediaTypeNames.Application.Json)
            .Produces<int>();

        topicEndpointRouteBuilder.MapPut("topic", ([FromBody] UpdateTopicRequest updateTopicRequest,
                    ITopicService topicService, CancellationToken cancellationToken) =>
                Task.FromResult(topicService.UpdateAsync(updateTopicRequest, cancellationToken)))
            .Accepts<UpdateTopicRequest>(MediaTypeNames.Application.Json);

        topicEndpointRouteBuilder.MapDelete("topic", ([FromBody] DeleteTopicRequest deleteTopicRequest,
                    ITopicService topicService, CancellationToken cancellationToken) =>
                Task.FromResult(topicService.DeleteAsync(deleteTopicRequest, cancellationToken)))
            .Accepts<DeleteTopicRequest>(MediaTypeNames.Application.Json);

        return topicEndpointRouteBuilder;
    }
}