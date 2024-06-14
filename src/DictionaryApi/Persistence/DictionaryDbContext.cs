using System.Reflection;
using DictionaryApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi.Persistence;

public class DictionaryDbContext : IdentityDbContext<User>
{
    internal const string DictionarySchema = "Dictionary";
    private const string IdentitySchema = "Identity";
    
    public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options) { }

    public DbSet<Topic> Topics => base.Set<Topic>();
    public DbSet<SubTopic> SubTopics => base.Set<SubTopic>();
    public DbSet<Entry> Entries => base.Set<Entry>();
    public DbSet<Multimedia> Multimedia => base.Set<Multimedia>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // default schema will be used for identity tables, schemas for other tables
        // should be specified manually using ToTable method.
        builder.HasDefaultSchema(IdentitySchema);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}