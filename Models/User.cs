using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace RibbleChatServer.Models
{
    public class User : IdentityUser<Guid>
    {
        public User(string firstname, string lastname, string username, string email) => (FirstName, LastName, UserName, Email) = (firstname, lastname, username, email);
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

