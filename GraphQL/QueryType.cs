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
            // does the type even matter?
            descriptor
                .Field(query => query.Users)
                .UsePaging<UserType>()
                .Type<NonNullType<ListType<NonNullType<UserType>>>>();

            descriptor
                .Field(query => query.Groups)
                .UsePaging<GroupType>()
                .Type<NonNullType<ListType<NonNullType<GroupType>>>>();


        }

    }

    public class Query
    {

        private MainDbContext UserDb;
        public Query(MainDbContext userDb)
        {
            this.UserDb = userDb;
        }

        public IQueryable<User> Users => UserDb.Users.Include(user => user.Groups);
        public IQueryable<Group> Groups => UserDb.Groups.Include(group => group.Users);
    }


}


