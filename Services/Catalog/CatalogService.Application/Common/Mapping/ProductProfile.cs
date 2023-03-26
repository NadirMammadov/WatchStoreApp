using CatalogService.Application.ProductCQRS.Queries;

namespace CatalogService.Application.Common.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, NewProductsDto>().ReverseMap();
        }
    }
}
