using System;
using HotChocolate.Types;

namespace RibbleChatServer.GraphQL
{
    public class SubscriptionType : ObjectType<Subscription>
    {
        protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
        {
            descriptor.Field("hello");
        }
    }

    public class Subscription { }
}
