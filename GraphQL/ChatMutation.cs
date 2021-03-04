using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using RibbleChatServer.Data;

namespace RibbleChatServer.GraphQL
{
    public partial class Mutation
    {

        public record SendMessageInput(int todo);
        public record SendMessagePayload(int todo);

        public async Task<SendMessagePayload> SendMessage(
            SendMessageInput input,
            [ScopedService] MainDbContext dbContext,
            [Service] ITopicEventSender eventSender
        )
        {
            await eventSender.SendAsync("new-message", 1);
            return new SendMessagePayload(3);
        }

        public record TestMutationInput(int x);
        public record TestMutationPayload(int y);

        public async Task<TestMutationPayload> TestMutation(
            TestMutationInput input,
            [Service] ITopicEventSender eventSender
        )
        {
            await eventSender.SendAsync("test-event", input.x);
            return new TestMutationPayload(input.x + 1);
        }

    }
}