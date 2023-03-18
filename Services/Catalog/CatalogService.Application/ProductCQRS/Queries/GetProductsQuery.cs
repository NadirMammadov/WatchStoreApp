using AutoMapper;
using CatalogService.Application.CategoryCQRS.Queries;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using WastchStore.Shared.Dtos;

namespace CatalogService.Application.ProductCQRS.Queries
{
    public class GetProductsQuery : IRequest<Response<List<ProductListDto>>> { }


    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<List<ProductListDto>>>
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IDatabaseSettings _databaseSettings;
        public GetProductsQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings, ISender mediator)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _mediator = mediator;
        }
        public async Task<Response<List<ProductListDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            if (products.Any())
            {
                foreach (var product in products)
                {
                    var category = await _mediator.Send(new GetCategoryQuery(product.CategoryId));
                    product.Category = new Category { Id = category.Data.Id, Name = category.Data.Name };
                }
            }
            return Response<List<ProductListDto>>.Success(_mapper.Map<List<ProductListDto>>(products), 200);
        }
    }
}
