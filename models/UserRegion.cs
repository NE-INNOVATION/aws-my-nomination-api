using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_nomination_api.models
{
    public class UserRegion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("enterpriseId")]
        public string EnterpriseId { get; set; }

        [BsonElement("emailId")]
        public string EmailId { get; set; }

        [BsonElement("regionId")]
        public string RegionId { get; set; }

        [BsonElement("regionName")]
        public string RegionName { get; set; }
    }
}
