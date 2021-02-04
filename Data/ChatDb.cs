using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RibbleChatServer.Models;

namespace RibbleChatServer.Data
{
    public class ChatDbContext : IdentityDbContext<User, Role, int>
    {
        public ChatDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

    }
}
