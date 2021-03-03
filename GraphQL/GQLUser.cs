using HotChocolate.Types;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(user => user.Id)
                .ResolveNode(async (context, id) =>
                    await context.Service<UserDbContext>().Users.FindAsync(id));
        }
    }
}


