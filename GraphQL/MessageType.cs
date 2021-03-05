using GraphQL.Types;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GQLMessage : ObjectGraphType<ChatMessage>
    {
        public GQLMessage()
        {
            Field(msg => msg.AuthorUsername);
            Field(msg => msg.Content);
            Field(msg => msg.AuthorId);
            Field(msg => msg.Timestamp);
        }
    }
}
