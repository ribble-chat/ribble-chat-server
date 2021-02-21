using Cassandra;
using Microsoft.AspNetCore.SignalR;
using RibbleChatServer.Data;
using RibbleChatServer.Models;
using System;
using System.Threading.Tasks;

namespace RibbleChatServer.Services
{
    public class ChatHub : Hub
    {
        private IChatDb chatDb;

        public ChatHub(IChatDb chatDb)
        {
            this.chatDb = chatDb;
        }

        public async Task JoinGroup(string groupName, string connectionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("joined-group", groupName, Context.ConnectionId);
        }

        public async Task SendMessage(SendMessageRequest request)
        {
            var (authorId, authorName, groupId, content) = request;
            var message = new ChatMessage(
                GroupId: groupId,
                MessageId: TimeUuid.NewId(),
                Timestamp: DateTimeOffset.UtcNow,
                AuthorName: authorName,
                AuthorId: authorId,
                Content: content
            );
            await Clients.Group(groupId.ToString())
                .SendAsync("message-received", message);
            await chatDb.AddMessage(message);
        }
    }

}
