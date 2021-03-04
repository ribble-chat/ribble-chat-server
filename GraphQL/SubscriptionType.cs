using System;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using RibbleChatServer.Models;

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

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<ChatMessage>> OnMessageSent(
            [Service] ITopicEventReceiver eventReceiver,
            Guid groupId
        ) => await eventReceiver.SubscribeAsync<Topic, ChatMessage>(new Topic.NewMessage(groupId));
    }
}
