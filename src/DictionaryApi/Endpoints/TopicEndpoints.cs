using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using DictionaryApi.Entities;
using DictionaryApi.Extensions;
using DictionaryApi.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

public static class TopicEndpoints
{
    public static IEndpointRouteBuilder MapTopicApi(this IEndpointRouteBuilder topicEndpointRouteBuilder)
    {
        topicEndpointRouteBuilder.MapPost("topic/add", async ([FromBody] AddTopicRequest addTopicRequest,
                IDocumentSession documentSession,CancellationToken cancellationToken) =>
            {
                var georgianTranslation = addTopicRequest[Language.Ka];
                var englishTranslation = addTopicRequest[Language.Ka];
                var topicExists = await documentSession.Query<Topic>()
                    .AnyAsync(topic => topic.NameTranslations.Any(), cancellationToken);

                if (topicExists)
                    return Results.BadRequest("topic already exists");

                var topic = new Topic(Guid.NewGuid(),addTopicRequest.NameTranslations.ToTranslations());
                documentSession.Store(topic);

                await documentSession.SaveChangesAsync(cancellationToken);

                return Results.Ok(topic.Id);
            })
            .Accepts<AddTopicRequest>(MediaTypeNames.Application.Json)
            .Produces<int>()
            .WithOpenApi();

        return topicEndpointRouteBuilder;
    }
}