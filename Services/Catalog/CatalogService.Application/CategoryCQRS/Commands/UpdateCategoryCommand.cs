namespace CatalogService.Application.CategoryCQRS.Commands
{
    public class UpdateCategoryCommand : IRequest<TResponse<bool>>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, TResponse<bool>>
    {
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public UpdateCategoryCommandHandler(ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }
        public async Task<TResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
                return TResponse<bool>.Fail("Category not found", 404);
            }

            return TResponse<bool>.Success(204);
        }
    }
}
