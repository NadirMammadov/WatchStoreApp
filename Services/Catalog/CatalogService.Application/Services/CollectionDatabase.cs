namespace CatalogService.Application.Services
{
    public class CollectionDataBase<T> : ICollectionDatabase<T>
    {
        private IMongoCollection<T> _databaseCollection;
        private readonly IDatabaseSettings _databaseSettings;

        public CollectionDataBase(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IMongoCollection<T> GetMongoCollection()
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            if (typeof(T) == typeof(Product))
            {
                _databaseCollection = database.GetCollection<T>(_databaseSettings.ProductCollectionName);
            }
            else if (typeof(T) == typeof(Category))
            {
                _databaseCollection = database.GetCollection<T>(_databaseSettings.CategoryCollectionName);
            }
            return _databaseCollection;
        }
    }
}
