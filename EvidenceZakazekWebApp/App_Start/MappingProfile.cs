using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(pd => pd.SupplierName, opt => opt.MapFrom(p => p.Supplier.Name))
                .ForMember(pd => pd.CategoryName, opt => opt.MapFrom(p => p.ProductCategory.Name));

            CreateMap<ProductDto, ProductTableDto>()
                .ForMember(ptd => ptd.StaticProperties, opt => opt.MapFrom(pd =>
                    new Dictionary<string, string>() {
                        { "Dodavatel", pd.SupplierName },
                        { "Název", pd.Name },
                        { "Kategorie", pd.CategoryName },
                        { "Objednací číslo", pd.OrderNumber },
                        { "Typové označení", pd.TypeName },
                        { "Cena", pd.Price.ToString() }
                    }))
                .ForMember(ptd => ptd.DynamicProperties, opt => opt.MapFrom(pd => pd.PropertyValues
                    .ToDictionary(pvd => pvd.PropertyDefinitionName, pvd => pvd.Value)))
                .ForMember(ptd => ptd.Properties, opt => opt.Ignore())
                .ForMember(ptd => ptd.ColumnNames, opt => opt.Ignore());

            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<ProductCategoryDto, ProductCategoryTableDto>()
                .ForMember(pctd => pctd.StaticProperties, opt => opt.MapFrom(pcd =>
                    new Dictionary<string, string>() {
                        { "Název", pcd.Name }
                    }))
                .ForMember(ptd => ptd.Properties, opt => opt.Ignore())
                .ForMember(ptd => ptd.ColumnNames, opt => opt.Ignore());

            CreateMap<PropertyDefinition, PropertyDefinitionDto>();

            CreateMap<PropertyValue, PropertyValueDto>();
        }
    }
}