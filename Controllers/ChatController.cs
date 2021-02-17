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
        [Route("/api/chat/group")]
        public async Task<ActionResult<GroupResponse>> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var (groupName, userIds) = request;
            // var userGroups = userIds.Select(userId => new UserGroup { UserId = userId, });
            var newGroup = new Group { Name = groupName };
            var entity = await userDb.AddAsync(newGroup);
            var group = entity.Entity;
            group.Users.AddRange(userIds.Select(userId => userDb.Users.Find(userId)));
            await userDb.SaveChangesAsync();
            return (GroupResponse)group;
        }
    }
}