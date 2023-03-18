using AutoMapper;
using CatalogService.Application.CategoryCQRS.Queries;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Mapping;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryListDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }


}
