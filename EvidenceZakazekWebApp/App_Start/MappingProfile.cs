using AutoMapper;
using EvidenceZakazekWebApp.Dtos;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Linq;

namespace EvidenceZakazekWebApp.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product
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

            CreateMap<ProductFormViewModel, Product>()
                .ForMember(p => p.Supplier, opt => opt.Ignore())
                .ForMember(p => p.ProductCategory, opt => opt.Ignore());

            CreateMap<Product, ProductFormViewModel>()
                .ForMember(pfvm => pfvm.Heading, opt => opt.Ignore())
                .ForMember(pfvm => pfvm.Suppliers, opt => opt.Ignore())
                .ForMember(pfvm => pfvm.ProductCategories, opt => opt.Ignore());

            // ProductCategory
            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<ProductCategoryDto, ProductCategoryTableDto>()
                .ForMember(pctd => pctd.StaticProperties, opt => opt.MapFrom(pcd =>
                    new Dictionary<string, string>() {
                        { "Název", pcd.Name }
                    }))
                .ForMember(ptd => ptd.Properties, opt => opt.Ignore())
                .ForMember(ptd => ptd.ColumnNames, opt => opt.Ignore());

            CreateMap<ProductCategoryFormViewModel, ProductCategory>()
                .ForMember(pc => pc.Products, opt => opt.Ignore());

            CreateMap<ProductCategory, ProductCategoryFormViewModel>()
                .ForMember(pcfvm => pcfvm.Heading, opt => opt.Ignore());


            // PropertyDefinition
            CreateMap<PropertyDefinition, PropertyDefinitionDto>();

            CreateMap<PropertyDefinitionFormViewModel, PropertyDefinition>()
                .ForMember(pd => pd.ProductCategory, opt => opt.Ignore())
                .ForMember(pd => pd.ProductCategoryId, opt => opt.Ignore())
                .ForMember(pd => pd.PropertyValues, opt => opt.Ignore());

            CreateMap<PropertyDefinition, PropertyDefinitionFormViewModel>();



            // PropertyValue
            CreateMap<PropertyValue, PropertyValueDto>();

            CreateMap<PropertyValueFormViewModel, PropertyValue>()
                .ForMember(pv => pv.Id, opt => opt.Ignore())
                .ForMember(pv => pv.PropertyDefinition, opt => opt.Ignore())
                .ForMember(pv => pv.Product, opt => opt.Ignore())
                .ForMember(pv => pv.ProductId, opt => opt.Ignore());

            CreateMap<PropertyValue, PropertyValueFormViewModel>()             
                .ForMember(pv => pv.PropertyDefinitionName, opt => opt.MapFrom( pv => 
                    pv.PropertyDefinition.Name))
                .ForMember(pv => pv.MeasureUnit, opt => opt.MapFrom(pv =>
                    pv.PropertyDefinition.MeasureUnit));
        }
    }
}