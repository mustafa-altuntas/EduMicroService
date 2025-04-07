using MongoDB.Bson.Serialization.Attributes;

namespace EduMicroService.Catalog.Api.Repositories
{
    public class BaseEntity
    {
        // snowflake id
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
