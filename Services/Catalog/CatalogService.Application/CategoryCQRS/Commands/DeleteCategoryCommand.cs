namespace CatalogService.Application.CategoryCQRS.Commands
{
    public class DeleteCategoryCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public DeleteCategoryCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<bool>>
    {
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public DeleteCategoryCommandHandler(ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }
        public async Task<Response<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            var result = await _categoryCollection.DeleteOneAsync(x => x.Id == request.Id);
            if (result.DeletedCount > 0)
            {
                return Response<bool>.Success(204);
            }
            else
            {
                return Response<bool>.Fail("Category not found", 404);
            }
        }
    }
}
