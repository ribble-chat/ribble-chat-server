using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace RibbleChatServer.Models
{
    public class User : IdentityUser<Guid>
    {
        public User(string FirstName, string LastName, string UserName, string Email) =>
            (this.FirstName, this.LastName, this.UserName, this.Email) = (FirstName, LastName, UserName, Email);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public static explicit operator UserResponse(User g) => new UserResponse(
            Id: g.Id,
            FirstName: g.FirstName,
            LastName: g.LastName,
            UserName: g.UserName,
            Email: g.Email,
            Groups: g.Groups
        );
    }

    public record UserResponse(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("firstname")] string FirstName,
        [property: JsonPropertyName("lastname")] string LastName,
        [property: JsonPropertyName("username")] string UserName,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("groups")] List<Group> Groups
    );

    public record RegisterUserInfo(
        [Required] string Email,
        [Required] string Username,
        [Required] string FirstName,
        [Required] string LastName,
        [Required] string Password
    );

    public record LoginUserInfo
    {
        [Required]
        public string UsernameOrEmail { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

