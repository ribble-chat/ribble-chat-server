using System.Linq;
using HotChocolate.Data;
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
                .Type<NonNullType<ListType<NonNullType<UserType>>>>();
            // .UsePaging<NonNull<UserType>>();

            descriptor
                .Field(query => query.Groups)
                .Type<NonNullType<ListType<NonNullType<GroupType>>>>();
            // .UsePaging<NonNull<GroupType>>();


        }

    }

    public class Query
    {
        private MainDbContext UserDb;

        public Query(MainDbContext userDb)
        {
            this.UserDb = userDb;
        }

        [UseFiltering]
        public IQueryable<User> Users => UserDb.Users.Include(user => user.Groups);

        [UseFiltering]
        public IQueryable<Group> Groups => UserDb.Groups.Include(group => group.Users);
    }


}


