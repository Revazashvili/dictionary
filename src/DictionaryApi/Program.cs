using DictionaryApi;
using DictionaryApi.Endpoints;
using DictionaryApi.Entities;
using Marten;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var connectionString = builder.Configuration.GetConnectionString("DictionaryDb");
builder.Services.AddDbContext<DictionaryDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.DatabaseSchemaName = "dictionary";
});

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<DictionaryDbContext>()
    .AddApiEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var apiGroup = app.MapGroup("/api");
apiGroup.MapIdentityApi<User>();
apiGroup.MapTopicApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();