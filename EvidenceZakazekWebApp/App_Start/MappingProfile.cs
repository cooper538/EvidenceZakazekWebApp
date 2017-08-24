using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Models;

namespace EvidenceZakazekWebApp.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(pd => pd.SupplierName, opt => opt.MapFrom(p => p.Supplier.Name))
                .ForMember(pd => pd.CategoryName, opt => opt.MapFrom(p => p.ProductCategory.Name));


            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<PropertyDefinition, PropertyDefinitionDto>();
        }
    }
}