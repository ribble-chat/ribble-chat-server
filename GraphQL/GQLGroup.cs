using GraphQL.Types;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GQLGroup : ObjectGraphType<Group>
    {
        public GQLGroup()
        {
            Field(group => group.Id);
            Field(group => group.Name);
            Field(group => group.Users, type: typeof(UserType));
        }
    }
}


