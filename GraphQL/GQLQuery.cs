using System.Linq;
using Microsoft.EntityFrameworkCore;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GQLQuery
    {

        private UserDbContext UserDb;
        public GQLQuery(UserDbContext userDb)
        {
            this.UserDb = userDb;
        }

        public IQueryable<User> GetUsers() => UserDb.Users.Include(user => user.Groups);
    }

}


