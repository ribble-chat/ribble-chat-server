using HotChocolate.Types;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public class GroupType : ObjectType<Group>
    {

        protected override void Configure(IObjectTypeDescriptor<Group> descriptor)
        {
            base.Configure(descriptor);
            descriptor
                .ImplementsNode()
                .IdField(group => group.Id)
                .ResolveNode(async (ctx, id) =>
                    await ctx.Service<UserDbContext>().Groups.FindAsync(id));
        }
    }
}


