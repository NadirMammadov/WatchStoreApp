namespace CatalogService.Application.CategoryCQRS.Queries
{
    public class GetCategoriesQuery : IRequest<Response<List<CategoryListDto>>> { }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<List<CategoryListDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICollectionDatabase<Category> _categoryCollectionDatabase;
        public GetCategoriesQueryHandler(IMapper mapper, ICollectionDatabase<Category> categoryCollectionDatabase)
        {
            _mapper = mapper;
            _categoryCollectionDatabase = categoryCollectionDatabase;
        }
        public async Task<Response<List<CategoryListDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var _categoryCollection = _categoryCollectionDatabase.GetMongoCollection();
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            if (!categories.Any())
            {
                categories = new List<Category>();
            }
            return Response<List<CategoryListDto>>.Success(_mapper.Map<List<CategoryListDto>>(categories), 200);
        }
    }
}
