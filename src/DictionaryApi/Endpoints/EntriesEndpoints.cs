using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using DictionaryApi.EndpointFilters;
using DictionaryApi.Entities;
using DictionaryApi.Models;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.Endpoints;

internal static class EntriesEndpoints
{
    internal static void MapEntriesApi(this IEndpointRouteBuilder entriesEndpointRouteBuilder)
    {
        entriesEndpointRouteBuilder.MapGet("entry",
            ([Required] int pageNumber, [Required] int pageSize, string searchText, int? subTopicId,
                    EntityStatus? status, IEntryService entryService, CancellationToken cancellationToken) =>
                entryService.GetAllAsync(new EntryFilterModelWithPagination(pageNumber, pageSize, searchText, subTopicId, status), cancellationToken));
        
        entriesEndpointRouteBuilder.MapGet("entry/count",
            (string searchText, int? subTopicId, EntityStatus? status, IEntryService entryService, CancellationToken cancellationToken) =>
                entryService.GetCountAsync(new BaseEntryFilterModel(searchText, subTopicId, status), cancellationToken));
        
        entriesEndpointRouteBuilder.MapGet("entry/{id:int}",
            async ([Required] int id, IEntryService entryService, CancellationToken cancellationToken) => 
            id == 0 ? Results.BadRequest("id is not valid")
                : Results.Ok(await entryService.GetByIdAsync(id, cancellationToken)));

        entriesEndpointRouteBuilder.MapPost("entry", async ([FromBody] AddEntryRequest addEntryRequest,
                IEntryService entryService, CancellationToken cancellationToken) =>
            {
                addEntryRequest.Validate();
                var entryId = await entryService.AddAsync(addEntryRequest, cancellationToken);

                return Results.Ok(entryId);
            })
            .Accepts<AddTopicRequest>(MediaTypeNames.Application.Json)
            .Produces<int>()
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();

        entriesEndpointRouteBuilder.MapPut("entry", async ([FromBody] UpdateEntryRequest updateEntryRequest,
                IEntryService entryService, CancellationToken cancellationToken) =>
            {
                updateEntryRequest.Validate();
                await entryService.UpdateAsync(updateEntryRequest, cancellationToken);

                return Results.Ok();
            })
            .Accepts<UpdateTopicRequest>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<AdminPrivilegesEndpointFilter>();

        entriesEndpointRouteBuilder.MapDelete("entry/{id:int}",
                async (int id, IEntryService entryService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await entryService.DeleteAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<SuperAdminPrivilegesEndpointFilter>();
        
        entriesEndpointRouteBuilder.MapPut("entry/activate/{id:int}",
                async (int id, IEntryService entryService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await entryService.ActivateAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<SuperAdminPrivilegesEndpointFilter>();
        
        entriesEndpointRouteBuilder.MapPut("entry/deactivate/{id:int}",
                async (int id, IEntryService entryService, CancellationToken cancellationToken) =>
                {
                    if (id == 0)
                        return Results.BadRequest("id is not valid");

                    await entryService.DeactivateAsync(id, cancellationToken);
                    return Results.Ok();
                })
            .Accepts<int>(MediaTypeNames.Application.Json)
            .AddEndpointFilter<SuperAdminPrivilegesEndpointFilter>();

    }
}