using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types.Relay;
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

        public record JoinGroupInput(Guid GroupId, Guid UserId);
        public record JoinGroupPayload(Group Group);

        public async Task<JoinGroupPayload> JoinGroup(
            JoinGroupInput input,
            [ScopedService] MainDbContext db,
            [Service] ITopicEventSender eventSender
        )
        {
            var (groupId, userId) = input;
            var group = await db.Groups.FindAsync(groupId);
            var user = await db.Users.FindAsync(userId);
            group.Users.Add(user);
            await db.AddAsync(group);
            await db.SaveChangesAsync();
            return new JoinGroupPayload(group);
        }

        public record CreateGroupInput(
            string GroupName,
            [ID] List<Guid> UserIds
        );

        public record CreateGroupPayload(Group Group);

        public async Task<CreateGroupPayload> CreateGroup(
            CreateGroupInput input,
            [Service] MainDbContext db,
            [Service] ITopicEventSender eventSender
        )
        {
            var (groupName, userIds) = input;
            var newGroup = new Group(groupName);
            newGroup.Users.AddRange(userIds.Select(userId => db.Users.Find(userId)));
            var entry = await db.AddAsync(newGroup);
            await db.SaveChangesAsync();
            return new CreateGroupPayload(entry.Entity);
        }




    }
}