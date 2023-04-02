using WatchStore.Shared.Messages;
using Mas = MassTransit;
namespace CatalogService.Application.ProductCQRS.Commands
{
    public class UpdateProductCommand : IRequest<TResponse<NoContent>>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Brend { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string CorpusSize { get; set; } = null!;
        public string Mechanism { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public decimal Price { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, TResponse<NoContent>>
    {
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        private readonly Mas.IPublishEndpoint _publishEndpoint;

        public UpdateProductCommandHandler(ICollectionDatabase<Product> productCollectionDatabase, Mas.IPublishEndpoint publishEndpoint)
        {
            _productCollectionDatabase = productCollectionDatabase;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<TResponse<NoContent>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            Product product = new()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Brend = request.Brend,
                Model = request.Model,
                CorpusSize = request.CorpusSize,
                Mechanism = request.Mechanism,
                Picture = request.Picture,
                CategoryId = request.CategoryId,
                Price = request.Price,
                LastModified = DateTime.Now
            };
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
            if (result == null)
            {
                return TResponse<NoContent>.Fail("Product not found", 404);
            }
            await _publishEndpoint.Publish<ProductNameChangeEvent>(new ProductNameChangeEvent
            {
                ProductId = product.Id,
                UpdatedName = product.Name
            });
            return TResponse<NoContent>.Success(204);
        }
    }
}
