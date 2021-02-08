using Microsoft.AspNetCore.SignalR;
using RibbleChatServer.Data;
using System.Threading.Tasks;

namespace RibbleChatServer.Services
{
    public class ChatHub : Hub
    {
        private ChatDb chatDb;

        public ChatHub(ChatDb chatDb)
        {
            this.chatDb = chatDb;
        }

        public async Task JoinGroup(string groupName, string connectionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("joined-group", groupName, Context.ConnectionId);
        }

        public async Task SendMessageToGroup(string groupName, string msg)
        {
            await Clients.Group(groupName).SendAsync("message-received", Context.ConnectionId, msg);
        }
    }
}
