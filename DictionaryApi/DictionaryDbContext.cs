using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApi;

public class DictionaryDbContext : IdentityDbContext<User>
{
    public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options) { }
}