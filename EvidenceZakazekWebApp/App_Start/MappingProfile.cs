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
                .ForMember(pd => pd.SupplierName, opt => opt.MapFrom(p => p.Supplier.Name));

            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<PropertyDefinition, PropertyDefinitionDto>();
        }
    }
}