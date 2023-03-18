using CatalogService.Application.CategoryCQRS.Queries;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using WastchStore.Shared.Dtos;

namespace CatalogService.Application.CategoryCQRS.Commands
{
    public class CreateCategoryCommand : IRequest<Response<CategoryDto>>
    {
        public string Name { get; set; } = null!;
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryDto>>
    {

        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IDatabaseSettings _databaseSettings;

        public CreateCategoryCommandHandler(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }

        public async Task<Response<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryCollection.InsertOneAsync(new Category()
            {
                Name = request.Name
            });
            return Response<CategoryDto>.Success(201);
        }
    }
}
