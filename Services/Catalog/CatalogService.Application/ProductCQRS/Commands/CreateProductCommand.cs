namespace CatalogService.Application.ProductCQRS.Commands
{
    public class CreateProductCommand : IRequest<Response<bool>>
    {
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
    public class CreateCategoryCommandHandler : IRequestHandler<CreateProductCommand, Response<bool>>
    {

        private readonly ICollectionDatabase<Product> _productCollectionDatabase;

        public CreateCategoryCommandHandler(ICollectionDatabase<Product> productCollectionDatabase)
        {
            _productCollectionDatabase = productCollectionDatabase;
        }

        public async Task<Response<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
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
                Created = DateTime.Now
            });
            return Response<bool>.Success(201);
        }
    }
}
