using Cassandra;
using System;
using Cassandra.Mapping.Attributes;
using Cassandra.Mapping;

namespace RibbleChatServer.Models
{
    public record SendMessageRequest(long AuthorId, string AuthorName, TimeUuid GroupId, string content);

    [Table("messages")]
    public record ChatMessage
    (
        [property:PartitionKey]
        [property:Column("group_id")]
        TimeUuid GroupId,

        [property:ClusteringKey(0, SortOrder.Descending)]
        [property:Column("message_id")]
        TimeUuid MessageId,

        [property:Column("author_id")]
        long AuthorId,

        [property:Column("time_stamp")]
        DateTimeOffset Timestamp,

        [property:Column("author_name")]
        string AuthorName,

        [property:Column("content")]
        string Content
    );



}
