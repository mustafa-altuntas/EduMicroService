using MongoDB.Bson.Serialization.Attributes;

namespace EduMicroService.Discount.Api.Repositories
{
    public class BaseEntity
    {
        // snowflake id
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
