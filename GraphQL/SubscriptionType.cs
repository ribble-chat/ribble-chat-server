using System;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace RibbleChatServer.GraphQL
{
    public class SubscriptionType : ObjectType<Subscription>
    {
    }

    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<int>> OnTestEvent(
            [Service] ITopicEventReceiver eventReceiver
        ) => await eventReceiver.SubscribeAsync<Topic, int>(new Topic.Test());
    }
}
