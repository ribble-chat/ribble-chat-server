using System;
using GraphQL.Types;

namespace RibbleChatServer.GraphQL
{
    public class ChatSchema : Schema
    {

        public ChatSchema(IServiceProvider provider) : base(provider)
        {
            Query = new ChatQuery();
            // Mutation = new ChatMutation();
            // Subscription = new ChatSubscription();
        }
    }
}
