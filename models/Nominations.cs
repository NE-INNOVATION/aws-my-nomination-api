using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_nomination_api.models
{
    public class Nominations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("enterpriseId")]
        public string EnterpriseId { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("level")]
        public string Level { get; set; }

        [BsonElement("project")]
        public string Project { get; set; }

        [BsonElement("bussinessGroup")]
        public string BussinessGroup { get; set; }

        [BsonElement("clientName")]
        public string ClientName { get; set; }

        [BsonElement("managerId")]
        public string ManagerId { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("primarySkill")]
        public string PrimarySkill { get; set; }

        [BsonElement("secondarySkill")]
        public string SecondarySkill { get; set; }

        [BsonElement("programId")]
        public string ProgramId { get; set; }

        [BsonElement("approved")]
        public bool Approved { get; set; }

        [BsonElement("approver")]
        public string Approver { get; set; }
    }

    public class MoveNominations
    {
        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("nominations")]
        public List<Nominations> Nominations { get; set; }

        [BsonElement("program")]
        public NominationProgram Program { get; set; }
    }
}
