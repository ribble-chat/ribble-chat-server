using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RibbleChatServer.Models
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public record RegisterUserInfo
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Password { get; set; }
    }

    public class LoginUserInfo
    {
        [Required]
        public string? UsernameOrEmail { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

