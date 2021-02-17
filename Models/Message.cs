using Cassandra;
using System;
using Cassandra.Mapping.Attributes;
using Cassandra.Mapping;

namespace RibbleChatServer.Models
{
    public record SendMessageRequest(long AuthorId, string AuthorName, TimeUuid GroupId, string content);

    [Table("messages")]
    public record ChatMessage
    {
        [PartitionKey]
        [ClusteringKey(0, SortOrder.Descending)]
        [Column("message_id")]
        public TimeUuid MessageId { get; set; }

        [Column("group_id")]
        [ClusteringKey(1, SortOrder.Descending)]
        public TimeUuid GroupId { get; set; }

        [Column("author_id")]
        public long AuthorId { get; set; }

        [Column("time_stamp")]
        public DateTimeOffset Timestamp { get; set; }

        [Column("author_name")]
        public string AuthorName { get; set; } = null!;

        [Column("content")]
        public string Content { get; set; } = null!;
    };


}
