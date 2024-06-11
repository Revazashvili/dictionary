using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DictionaryApi.Entities;

public class User : IdentityUser
{
    public UserStatus Status { get; set; }
    public string Role { get; set; }

    [NotMapped]
    public bool IsAdmin => Role == UserRoles.Admin;
    
    [NotMapped]
    public bool IsSuperAdmin => Role == UserRoles.SuperAdmin;
    
    [NotMapped]
    public bool IsViewer => Role == UserRoles.Viewer;
}

public static class UserRoles
{
    public static readonly string[] All = [ Viewer, Admin, SuperAdmin ];
    public const string Viewer = "viewer";
    public const string Admin = "admin";
    public const string SuperAdmin = "super_admin";
}