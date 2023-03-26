namespace CatalogService.Application.CategoryCQRS.Commands
{
    public class UpdateCategoryCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<bool>>
    {
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public UpdateCategoryCommandHandler(ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }
        public async Task<Response<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            Category category = new()
            {
                Id = request.Id,
                Name = request.Name,
                LastModified = DateTime.Now
            };
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == category.Id, category);
            if (result == null)
            {
                return Response<bool>.Fail("Category not found", 404);
            }

            return Response<bool>.Success(204);
        }
    }
}
