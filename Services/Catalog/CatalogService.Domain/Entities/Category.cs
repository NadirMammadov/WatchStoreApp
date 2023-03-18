using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities;
public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
}
