﻿namespace CatalogService.Application.CategoryCQRS.Queries
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
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public GetCategoryQueryHandler(IMapper mapper, ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _mapper = mapper;
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }

        public async Task<Response<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            Category category = await _categoryCollection.Find(ct => ct.Id == request.Id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
