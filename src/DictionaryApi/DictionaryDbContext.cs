using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi;

public class DictionaryDbContext : IdentityDbContext<User>
{
    public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // default schema will be used for identity tables, schemas for other tables
        // should be specified manually using ToTable method.
        builder.HasDefaultSchema("identity");
        
        base.OnModelCreating(builder);
    }
}