using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace my_nomination_api.models
{
    public class GroupConfig
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("groupId")]
        public Guid GroupId { get; set; }

        [BsonElement("groupName")]
        public string GroupName { get; set; }

        [BsonElement("isRegion")]
        public bool IsRegion { get; set; }
    }
}
