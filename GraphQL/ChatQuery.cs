using System.Collections.Generic;
using GraphQL.Types;
using RibbleChatServer.GraphQL;

namespace RibbleChatServer.GraphQL
{
    public class ChatQuery : ObjectGraphType
    {
        public ChatQuery()
        {
            Field<ListGraphType<GQLMessage>>("messages", resolve: context => new List<GQLMessage>());
            Field<ListGraphType<GQLUser>>("users", resolve: context => new List<GQLUser>());
        }
    }
}


