using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RibbleChatServer.Services
{
    public class ChatHub : Hub
    {
        public async Task JoinGroup(string groupName, string connectionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("joined-group", groupName, Context.ConnectionId);
        }
        public async Task SendMessageToGroup(string groupName, string msg)
        {
            await Clients.Group(groupName).SendAsync("sent-message-to-group", Context.ConnectionId, msg);
        }
    }
}