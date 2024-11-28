using System.Text.Json.Serialization;
using DictionaryApi.Endpoints;
using DictionaryApi.Entities;
using DictionaryApi.Persistence;
using DictionaryApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Extensions;

internal static class WebApplicationExtensions
{
    internal static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services.AddCors();

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
        services.AddScoped<IMultimediaService, MultimediaService>();

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

    internal static async Task Configure(this WebApplication app)
    {
        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
        );
        
        var apiGroup = app.MapGroup("/api");
        apiGroup.MapCustomIdentityApi();
        apiGroup.MapTopicApi();
        apiGroup.MapSubTopicApi();
        apiGroup.MapEntriesApi();
        apiGroup.MapMultimediaApi();

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
        
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var userStore = scope.ServiceProvider.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;
            const string email = "dictionaryadmin@gmail.com";
            var user = new User
            {
                Role = UserRoles.SuperAdmin,
                Status = UserStatus.Active
            };
            
            await userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await userManager.CreateAsync(user, "Admin$1");

            var resultText = result.Succeeded
                ? "admin user successfully added to database"
                : "can't add admin user to database";

            Console.WriteLine(resultText);
        }
    }
}