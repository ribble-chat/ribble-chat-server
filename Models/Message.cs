using System;
using Cassandra;
using Cassandra.Mapping.Attributes;
using Cassandra.Mapping;
using System.Text.Json.Serialization;

namespace RibbleChatServer.Models
{
    public record SendMessageRequest(Guid AuthorId, string AuthorUsername, Guid GroupId, string content);

    [Table("messages")]
    public record ChatMessage
    (
        [property:PartitionKey]
        [property:Column("group_id")]
        Guid GroupId,

        [property:ClusteringKey(0, SortOrder.Descending)]
        [property:Column("time_stamp")]
        DateTimeOffset Timestamp,

        [property:Column("message_id")]
        [property:JsonPropertyName("id")]
        Guid MessageId,

        [property:Column("author_id")]
        Guid AuthorId,

        [property:Column("author_name")]
        string AuthorName,

        [property:Column("content")]
        string Content
    );



}
