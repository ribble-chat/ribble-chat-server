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
    }

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

