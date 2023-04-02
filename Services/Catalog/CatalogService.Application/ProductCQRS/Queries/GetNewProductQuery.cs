namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetNewProductQuery : IRequest<TResponse<List<NewProductsDto>>> { }
    public class GetNewProductQueryHandler : IRequestHandler<GetNewProductQuery, TResponse<List<NewProductsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        public GetNewProductQueryHandler(IMapper mapper, ICollectionDatabase<Product> collectionDatabase)
        {
            _mapper = mapper;
            _productCollectionDatabase = collectionDatabase;
        }
        public async Task<TResponse<List<NewProductsDto>>> Handle(GetNewProductQuery request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            var products = await _productCollection.Find(x => true).SortByDescending(x => x.Created).Limit(3).ToListAsync();
            return TResponse<List<NewProductsDto>>.Success(_mapper.Map<List<NewProductsDto>>(products), 200);
        }
    }
}
