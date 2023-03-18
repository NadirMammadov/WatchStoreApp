using AutoMapper;
using CatalogService.Application.Settings;
using CatalogService.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using WastchStore.Shared.Dtos;

namespace CatalogService.Application.CategoryCQRS.Queries
{
    public class GetCategoriesQuery : IRequest<Response<List<CategoryListDto>>> { }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<List<CategoryListDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IDatabaseSettings _databaseSettings;
        public GetCategoriesQueryHandler(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }
        public async Task<Response<List<CategoryListDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            if (!categories.Any())
            {
                categories = new List<Category>();
            }
            return Response<List<CategoryListDto>>.Success(_mapper.Map<List<CategoryListDto>>(categories), 200);
        }
    }
}
