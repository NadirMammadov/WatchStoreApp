namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetProductCountQuery : IRequest<Response<ProductCountDto>> { }
    public class GetProductCountQueryHandler : IRequestHandler<GetProductCountQuery, Response<ProductCountDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        public GetProductCountQueryHandler(IMapper mapper, ICollectionDatabase<Product> productCollectionDatabase)
        {
            _mapper = mapper;
            _productCollectionDatabase = productCollectionDatabase;
        }
        public async Task<Response<ProductCountDto>> Handle(GetProductCountQuery request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            long productCount = await _productCollection.CountDocumentsAsync(product => true);
            var responseDto = new ProductCountDto { ProductCount = productCount };
            return Response<ProductCountDto>.Success(responseDto, 200);
        }
    }

    public class ProductCountDto
    {
        public long ProductCount { get; set; }
    }
}
