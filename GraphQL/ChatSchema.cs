using System;
using GraphQL.Types;

namespace RibbleChatServer.GraphQL
{
    public class ChatSchema : Schema
    {

        public ChatSchema(IServiceProvider provider, Query query) : base(provider)
        {
            // Query = query;
            // Mutation = new ChatMutation();
            // Subscription = new ChatSubscription();
        }
    }
}
