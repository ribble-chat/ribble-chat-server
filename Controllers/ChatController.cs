using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RibbleChatServer.Data;
using RibbleChatServer.Models;

namespace RibbleChatServer.Controllers
{

    [ApiController]
    public class ChatController
    {
        private readonly UserDbContext userDb;

        public ChatController(UserDbContext userDb) => this.userDb = userDb;

        [HttpPost]
        [Route("/api/chat/groups")]
        public async Task<ActionResult<GroupResponse>> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var (groupName, userIds) = request;
            var newGroup = new Group(name: groupName);
            var entity = await userDb.AddAsync(newGroup);
            var group = entity.Entity;
            group.Users.AddRange(userIds.Select(userId => userDb.Users.Find(userId)));
            await userDb.SaveChangesAsync();
            return (GroupResponse)group;
        }
    }
}