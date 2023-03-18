using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CatalogService.Domain.Common;
public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
}
