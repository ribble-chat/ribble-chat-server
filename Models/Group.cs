using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace RibbleChatServer.Models
{
    public record CreateGroupRequest(
        [Required] string GroupName,
        [Required] List<Guid> UserIds
    );

    public record GroupResponse
    {
        public GroupResponse(Guid id, string name, List<Guid> userIds) => (Id, Name, UserIds) = (id, name, userIds ?? new List<Guid>());

        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("userIds")]
        public List<Guid> UserIds { get; init; } = new List<Guid>();

        public static explicit operator GroupResponse(Group g) => new GroupResponse(
            id: g.Id,
            name: g.Name,
            g.Users.Select(user => user.Id).ToList()
        );
    }

    public record Group
    {
        public Group(string name) => Name = name;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }

}