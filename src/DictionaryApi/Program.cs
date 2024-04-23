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

builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IEntryService, EntryService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

var apiGroup = app.MapGroup("/api");
apiGroup.MapCustomIdentityApi();
apiGroup.MapTopicApi();
apiGroup.MapSubTopicApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var dictionaryDbContext = app.Services.CreateScope()
    .ServiceProvider.GetRequiredService<DictionaryDbContext>();
dictionaryDbContext.Database.Migrate();

app.Run();