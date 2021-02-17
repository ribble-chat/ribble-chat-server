using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RibbleChatServer.Models;

namespace RibbleChatServer.Data
{
    public class UserDbContext : IdentityDbContext<User, Role, int>
    {
        public UserDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
    }
}
