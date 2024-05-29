using Microsoft.AspNetCore.Identity;

namespace DictionaryApi.Entities;

public class User : IdentityUser
{
    public UserStatus Status { get; set; }
    public string Role { get; set; } //TODO: this should be enum

    public bool IdAdmin() => ((string[]) ["admin", "super_admin"]).Contains(Role);
}