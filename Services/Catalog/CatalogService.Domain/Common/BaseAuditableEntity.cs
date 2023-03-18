using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.Domain.Common;
public abstract class BaseAuditableEntity : BaseEntity
{
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }
    [BsonRepresentation(BsonType.DateTime)]

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
