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
    public UserResponse(string id, string email, UserStatus status)
    {
        Id = id;
        Email = email;
        Status = status;
    }

    public string Id { get; set; }
    public string Email { get; set; }
    public UserStatus Status { get; set; }
}