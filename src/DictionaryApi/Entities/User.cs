using Microsoft.AspNetCore.Identity;

namespace DictionaryApi.Entities;

public class User : IdentityUser
{
    public UserStatus Status { get; set; }
    public string Role { get; set; }

    public bool IdAdmin() => UserRoles.Admin.Contains(Role);
}

public static class UserRoles
{
    public static readonly string[] All = ["admin", "super_admin"];
    public static readonly string[] Admin = ["admin", "super_admin"];
}