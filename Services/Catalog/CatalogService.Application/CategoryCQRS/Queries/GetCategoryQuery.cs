namespace CatalogService.Application.CategoryCQRS.Queries
{
    public class GetCategoryQuery : IRequest<TResponse<CategoryDto>>
    {

        public GetCategoryQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; } = null!;

    }
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, TResponse<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public GetCategoryQueryHandler(IMapper mapper, ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _mapper = mapper;
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }

        public async Task<TResponse<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            Category category = await _categoryCollection.Find(ct => ct.Id == request.Id).FirstOrDefaultAsync();
            if (category == null)
            {
                return TResponse<CategoryDto>.Fail("Category not found", 404);
            }
            return TResponse<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
