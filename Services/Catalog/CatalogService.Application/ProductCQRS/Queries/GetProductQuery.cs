namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetProductQuery : IRequest<TResponse<ProductDto>>
    {
        public string Id { get; set; }
        public GetProductQuery(string id)
        {
            Id = id;
        }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, TResponse<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;

        public GetProductQueryHandler(IMapper mapper, ICollectionDatabase<Product> productCollectionDatabase, ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _mapper = mapper;
            _productCollectionDatabase = productCollectionDatabase;
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }

        public async Task<TResponse<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            var product = await _productCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (product != null)
            {
                var category = await _categoryCollection.Find(ct => ct.Id == product.CategoryId).FirstOrDefaultAsync();
                product.Category = new Category { Id = category.Id, Name = category.Name };
                return TResponse<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
            }
            return TResponse<ProductDto>.Fail("Not found product", 404);

        }
    }
}
