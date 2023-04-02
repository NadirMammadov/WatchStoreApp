namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetProductsQuery : IRequest<TResponse<List<ProductListDto>>>
    {
        public int Page { get; set; }
        public GetProductsQuery(int page)
        {
            Page = page;
        }
    }


    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, TResponse<List<ProductListDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public GetProductsQueryHandler(IMapper mapper, ICollectionDatabase<Product> collectionDatabase, ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _mapper = mapper;
            _productCollectionDatabase = collectionDatabase;
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }
        public async Task<TResponse<List<ProductListDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            var products = await _productCollection.Find(x => true).Skip((request.Page - 1) * 15).Limit(15).ToListAsync();
            if (products.Any())
            {
                foreach (var product in products)
                {
                    var category = await _categoryCollection.Find(x => x.Id == product.CategoryId).FirstOrDefaultAsync();
                    product.Category = new Category { Id = category.Id, Name = category.Name };
                }
            }
            return TResponse<List<ProductListDto>>.Success(_mapper.Map<List<ProductListDto>>(products), 200);
        }
    }
}
