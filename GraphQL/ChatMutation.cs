using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.GraphQL
{
    public partial class Mutation
    {
        public record SendMessageInput(
            Guid AuthorId,
            Guid GroupId,
            string AuthorUsername,
            string Content
        );

        public record SendMessagePayload(ChatMessage message);

        public async Task<SendMessagePayload> SendMessage(
            SendMessageInput input,
            [ScopedService] MainDbContext dbContext,
            [Service] MessageDb messageDb,
            [Service] ITopicEventSender eventSender
        )
        {
            var (authorId, groupId, authorName, content) = input;
            var message = new ChatMessage(
                MessageId: Guid.NewGuid(),
                Timestamp: DateTimeOffset.UtcNow,
                GroupId: groupId,
                AuthorId: authorId,
                AuthorName: authorName,
                Content: content
            );
            await eventSender.SendAsync(new Topic.NewMessage(groupId), message);
            await messageDb.AddMessage(message);
            return new SendMessagePayload(message);
        }

        public record TestMutationInput(int x);
        public record TestMutationPayload(int y);

        public async Task<TestMutationPayload> TestMutation(
            TestMutationInput input,
            [Service] ITopicEventSender eventSender
        )
        {
            await eventSender.SendAsync(new Topic.Test(), input.x);
            return new TestMutationPayload(input.x);
        }

    }
}