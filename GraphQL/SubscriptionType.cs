using System;
using HotChocolate.Types;
using HotChocolate.Subscriptions.InMemory;

namespace RibbleChatServer.GraphQL
{
    public class SubscriptionType : ObjectType<Subscription>
    {
        protected override void Configure(IObjectTypeDescriptor<Subscription> descriptor)
        {
            descriptor.Field(s => s.Test());
        }
    }

    public class Subscription
    {
        public int Test() { return 0;}
    }
}
