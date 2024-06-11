using System.ComponentModel.DataAnnotations;
using DictionaryApi.Entities;

namespace DictionaryApi.Models;

public class AddUserRequest
{
    [Required]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }
    
    [Required]
    public string Role { get; set; }
}

public class UserResponse
{
    public UserResponse(string id, 
        string email, 
        UserStatus status, 
        string role,
        bool isAdmin,
        bool isSuperAdmin,
        bool isViewer)
    {
        Id = id;
        Email = email;
        Status = status;
        Role = role;
        IsAdmin = isAdmin;
        IsSuperAdmin = isSuperAdmin;
        IsViewer = isViewer;
    }

    public string Id { get; set; }
    public string Email { get; set; }
    public UserStatus Status { get; set; }
    public string Role { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }
    public bool IsViewer { get; set; }
}