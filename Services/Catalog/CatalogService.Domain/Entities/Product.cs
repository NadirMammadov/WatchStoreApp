using CatalogService.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Brend { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string CorpusSize { get; set; } = null!;
        public string Mechanism { get; set; } = null!;
        public string Picture { get; set; } = null!;
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = null!;
        [BsonIgnore]
        public Category Category { get; set; } = null!;
    }
}
