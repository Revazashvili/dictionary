using System.Text.Json.Serialization;
using DictionaryApi.Endpoints;
using DictionaryApi.Entities;
using DictionaryApi.Persistence;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

var connectionString = builder.Configuration.GetConnectionString("DictionaryDb");
builder.Services.AddDbContext<DictionaryDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<DictionaryDbContext>()
    .AddApiEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<ITopicService, TopicService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

var apiGroup = app.MapGroup("/api");
apiGroup.MapIdentityApi<User>();
apiGroup.MapTopicApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();