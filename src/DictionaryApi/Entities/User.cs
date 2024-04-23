using Microsoft.AspNetCore.Identity;

namespace DictionaryApi.Entities;

public class User : IdentityUser
{
    public UserStatus Status { get; set; }
    public string Role { get; set; }
}