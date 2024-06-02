using System.Text.Json.Serialization;
using DictionaryApi.Endpoints;
using DictionaryApi.Entities;
using DictionaryApi.Persistence;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

await app.Configure();

app.Run();

public static class WebApplicationExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        var connectionString = configuration.GetConnectionString("DictionaryDb");
        services.AddDbContext<DictionaryDbContext>(options => 
            options.UseNpgsql(connectionString));

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<DictionaryDbContext>()
            .AddApiEndpoints();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IEntryService, EntryService>();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    }

    public static async Task Configure(this WebApplication app)
    {
        var apiGroup = app.MapGroup("/api");
        apiGroup.MapCustomIdentityApi();
        apiGroup.MapTopicApi();
        apiGroup.MapSubTopicApi();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        await app.MigrateDatabase();
    }

    private static async Task MigrateDatabase(this WebApplication app)
    {
        var dictionaryDbContext = app.Services.CreateScope()
            .ServiceProvider.GetRequiredService<DictionaryDbContext>();
        await dictionaryDbContext.Database.MigrateAsync();
    }
}