using CatalogService.Application.CategoryCQRS.Queries;
namespace CatalogService.Application.Common.Mapping;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryListDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }


}
