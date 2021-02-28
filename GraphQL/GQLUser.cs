using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GQLUser : ObjectGraphType<User>
    {
        public GQLUser(UserDbContext userDb)
        {
            Field(user => user.Id);
            Field(user => user.Email);
            Field(user => user.UserName);
            Field<ListGraphType<GQLGroup>>()
                .Name("groups")
                .ResolveAsync(async context =>
                {
                    var user = await userDb.Users
                    .Include(user => user.Groups)
                    .SingleAsync(user => user.Id == context.Source.Id);
                    return user.Groups;
                });

            // Field<ListGraphType<GQLGroup>>("groups", resolve: context =>
            //     userDb.Entry<User>(context.Source)
            //     .Reference(user => user.Groups)
            //     .Load());
        }
    }
}


