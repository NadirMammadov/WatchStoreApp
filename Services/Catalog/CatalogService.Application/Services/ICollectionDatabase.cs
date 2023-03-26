namespace CatalogService.Application.Services
{
    public interface ICollectionDatabase<T>
    {
        IMongoCollection<T> GetMongoCollection();
    }
}
