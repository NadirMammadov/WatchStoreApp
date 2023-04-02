using CatalogService.Application.CategoryCQRS.Queries;

namespace CatalogService.Application.CategoryCQRS.Commands
{
    public class CreateCategoryCommand : IRequest<TResponse<CategoryDto>>
    {
        public string Name { get; set; } = null!;
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, TResponse<CategoryDto>>
    {

        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public CreateCategoryCommandHandler(ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }

        public async Task<TResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            await _categoryCollection.InsertOneAsync(new Category()
            {
                Name = request.Name
            });
            return TResponse<CategoryDto>.Success(201);
        }
    }
}
