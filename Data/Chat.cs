using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace RibbleChatServer.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

    }
}
