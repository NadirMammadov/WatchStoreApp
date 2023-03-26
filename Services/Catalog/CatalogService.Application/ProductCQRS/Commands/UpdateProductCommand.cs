
namespace CatalogService.Application.ProductCQRS.Commands
{
    public class UpdateProductCommand : IRequest<Response<NoContent>>
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
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<NoContent>>
    {
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;

        public UpdateProductCommandHandler(ICollectionDatabase<Product> productCollectionDatabase)
        {
            _productCollectionDatabase = productCollectionDatabase;
        }
        public async Task<Response<NoContent>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
                LastModified = DateTime.Now
            };
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
            if (result == null)
            {
                return Response<NoContent>.Fail("Product not found", 404);
            }

            return Response<NoContent>.Success(204);
        }
    }
}
