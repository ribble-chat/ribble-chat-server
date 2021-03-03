using System.Linq;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            // https://github.com/ChilliCream/hotchocolate-docs/blob/master/docs/schema-object-type.md
            descriptor
                .Field(query => query.GetUsers())
                .Type<NonNullType<ListType<NonNullType<UserType>>>>();
        }

    }

    public class Query
    {

        private UserDbContext UserDb;
        public Query(UserDbContext userDb)
        {
            this.UserDb = userDb;
        }

        public IQueryable<User> GetUsers() => UserDb.Users.Include(user => user.Groups);
    }


}


