using System.Threading.Tasks;
using RibbleChatServer.Models;

namespace RibbleChatServer.Data
{
    public interface IChatDb
    {

        public Task AddMessage(ChatMessage msg);

    }
}
