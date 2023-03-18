using AutoMapper;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using WastchStore.Shared.Dtos;

namespace CatalogService.Application.CategoryCQRS.Queries
{
    public class GetCategoryQuery : IRequest<Response<CategoryDto>>
    {

        public GetCategoryQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; } = null!;

    }
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Response<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IDatabaseSettings _databaseSettings;
        public GetCategoryQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }
        public async Task<Response<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            Category category = await _categoryCollection.Find(ct => ct.Id == request.Id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
