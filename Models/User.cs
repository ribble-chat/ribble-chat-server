using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using RibbleChatServer.Models;

namespace RibbleChatServer.Models
{
    public class User : IdentityUser<Guid>
    {
        public User(string UserName, string Email) =>
            (this.UserName, this.Email) = (UserName, Email);

        public List<Group> Groups { get; set; } = new List<Group>();

        public static explicit operator UserResponse(User g) => new UserResponse(
            Id: g.Id,
            UserName: g.UserName,
            Email: g.Email,
            Groups: g.Groups.Select(g => (GroupResponse)g)
        );
    }

    public record UserResponse(
        [property: JsonPropertyName("id")] Guid Id,
        [property: JsonPropertyName("username")] string UserName,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("groups")] IEnumerable<GroupResponse> Groups
    );

    public record RegisterUserInfo(
        [Required] string Email,
        [Required] string Username,
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

