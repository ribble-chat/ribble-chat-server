using System.Collections.Generic;
using GraphQL.Types;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GQLQuery : ObjectGraphType
    {
        public GQLQuery(UserDbContext userDb)
        {
            Field<ListGraphType<GQLMessage>>("messages", resolve: context => new List<ChatMessage>());
            Field<ListGraphType<GQLUser>>("users", resolve: context => userDb.Users);
        }
    }
}


