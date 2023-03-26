using CatalogService.Application.CategoryCQRS.Queries;

namespace CatalogService.Application.CategoryCQRS.Commands
{
    public class CreateCategoryCommand : IRequest<Response<CategoryDto>>
    {
        public string Name { get; set; } = null!;
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryDto>>
    {

        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public CreateCategoryCommandHandler(ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }

        public async Task<Response<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            await _categoryCollection.InsertOneAsync(new Category()
            {
                Name = request.Name
            });
            return Response<CategoryDto>.Success(201);
        }
    }
}
