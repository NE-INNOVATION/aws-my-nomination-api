using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my_nomination_api.models
{
    public class NominationProgram
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("programId")]
        public string ProgramId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("startDate")]
        public string StartDate { get; set; }

        [BsonElement("endDate")]
        public string EndDate { get; set; }

        [BsonElement("courseAgenda")]
        public string CourseAgenda { get; set; }

        [BsonElement("banner")]
        public string Banner { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("isPublished")]
        public bool IsPublished { get; set; }

        [BsonElement("isClosed")]
        public bool IsClosed { get; set; }

        [BsonElement("status")]
        public int Status { get; set; }

        [BsonElement("categoryId")]
        public string categoryId { get; set; }

        [BsonElement("category")]
        public string category { get; set; }
    }

    public class ProgramCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }
    }

}
