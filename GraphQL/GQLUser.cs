using GraphQL.Types;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GQLUser : ObjectGraphType<User>
    {
        public GQLUser()
        {
            Field(user => user.Email);
            Field(user => user.UserName);
        }
    }
}

