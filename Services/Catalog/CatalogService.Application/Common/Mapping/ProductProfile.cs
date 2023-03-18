using AutoMapper;
using CatalogService.Application.ProductCQRS.Queries;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
