namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetNewProductQuery : IRequest<Response<List<NewProductsDto>>> { }
    public class GetNewProductQueryHandler : IRequestHandler<GetNewProductQuery, Response<List<NewProductsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        public GetNewProductQueryHandler(IMapper mapper, ICollectionDatabase<Product> collectionDatabase)
        {
            _mapper = mapper;
            _productCollectionDatabase = collectionDatabase;
        }
        public async Task<Response<List<NewProductsDto>>> Handle(GetNewProductQuery request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            var products = await _productCollection.Find(x => true).SortByDescending(x => x.Created).Limit(3).ToListAsync();
            return Response<List<NewProductsDto>>.Success(_mapper.Map<List<NewProductsDto>>(products), 200);
        }
    }
}
