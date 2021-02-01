using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RibbleChatServer.Services
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("message-received", user, message);
        }

        public async Task JoinGroup(string groupName, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("joined-group", groupName, Context.ConnectionId);
        }
    }
}