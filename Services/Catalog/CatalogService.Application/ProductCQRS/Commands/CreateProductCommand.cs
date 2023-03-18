using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using WastchStore.Shared.Dtos;

namespace CatalogService.Application.ProductCQRS.Commands
{
    public class CreateProductCommand : IRequest<Response<ProductDto>>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Brend { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string CorpusSize { get; set; } = null!;
        public string Mechanism { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDto>>
    {

        private readonly IMongoCollection<Product> _productCollection;
        private readonly IDatabaseSettings _databaseSettings;

        public CreateCategoryCommandHandler(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
        }

        public async Task<Response<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _productCollection.InsertOneAsync(new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Brend = request.Brend,
                CorpusSize = request.CorpusSize,
                Model = request.Model,
                Mechanism = request.Mechanism,
                CategoryId = request.CategoryId,
                Picture = request.Picture,
            });
            return Response<ProductDto>.Success(201);
        }
    }
}
