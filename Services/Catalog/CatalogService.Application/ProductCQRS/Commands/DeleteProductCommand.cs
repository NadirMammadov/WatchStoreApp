namespace CatalogService.Application.ProductCQRS.Commands
{
    public class DeleteProductCommand : IRequest<TResponse<bool>>
    {
        public string Id { get; set; }
        public DeleteProductCommand(string id)
        {
            Id = id;
        }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, TResponse<bool>>
    {
        private readonly ICollectionDatabase<Product> _productCollectionDatabase;
        public DeleteProductCommandHandler(ICollectionDatabase<Product> productCollectionDatabase)
        {
            _productCollectionDatabase = productCollectionDatabase;
        }
        public async Task<TResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var _productCollection = _productCollectionDatabase.GetMongoCollection();
            var result = await _productCollection.DeleteOneAsync(x => x.Id == request.Id);
            if (result.DeletedCount > 0)
            {
                return TResponse<bool>.Success(204);
            }
            else
            {
                return TResponse<bool>.Fail("Product not found", 404);
            }

        }
    }
}
