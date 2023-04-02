namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetProductCountQuery : IRequest<TResponse<ProductCountDto>> { }
    public class GetProductCountQueryHandler : IRequestHandler<GetProductCountQuery, TResponse<ProductCountDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        public GetProductCountQueryHandler(IMapper mapper, ICollectionDatabase<Product> productCollectionDatabase)
        {
            _mapper = mapper;
            _productCollectionDatabase = productCollectionDatabase;
        }
        public async Task<TResponse<ProductCountDto>> Handle(GetProductCountQuery request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            long productCount = await _productCollection.CountDocumentsAsync(product => true);
            var TResponseDto = new ProductCountDto { ProductCount = productCount };
            return TResponse<ProductCountDto>.Success(TResponseDto, 200);
        }
    }

    public class ProductCountDto
    {
        public long ProductCount { get; set; }
    }
}
