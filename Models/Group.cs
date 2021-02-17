using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace RibbleChatServer.Models
{
    public record CreateGroupRequest([Required] string GroupName, [Required] List<int> UserIds);

    public class GroupResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public List<int> UserIds { get; init; } = new List<int>();

        public static explicit operator GroupResponse(Group g) => new GroupResponse
        {
            Id = g.Id,
            Name = g.Name,
            UserIds = g.Users.Select(user => user.Id).ToList(),
        };
    }

    public record Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<User> Users { get; set; } = new List<User>();
    }

}