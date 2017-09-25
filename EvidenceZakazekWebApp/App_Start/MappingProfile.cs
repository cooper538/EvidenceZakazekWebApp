using AutoMapper;
using EvidenceZakazekWebApp.ViewModels;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.Linq;
using EvidenceZakazekWebApp.Core.Models;

namespace EvidenceZakazekWebApp.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product
            CreateMap<Product, CrudRowViewModel>()
                .ForMember(crvm => crvm.Properties, opt => opt.MapFrom(p =>
                    new Dictionary<string, string> {
                        { "Dodavatel", p.Supplier.Name },
                        { "Název", p.Name },
                        { "Kategorie", p.ProductCategory.Name },
                        { "Objednací číslo", p.OrderNumber },
                        { "Typové označení", p.TypeName },
                        { "Cena", p.Price.ToString() }}
                    .Concat(PropertyValuesToDict(p)).ToDictionary(s => s.Key, s => s.Value)
                    ));

            CreateMap<Product, DetailViewModel>()
                .ForMember(dvm => dvm.Properties, opt => opt.MapFrom(p =>
                    new Dictionary<string, string> {
                        { "Název", p.Name },
                        { "Dodavatel", p.Supplier.Name },
                        { "Objednací číslo", p.OrderNumber },
                        { "Typové označení", p.TypeName },
                        { "Cena", $"{p.Price} Kč"  }
                    }
                    .Concat(PropertyValuesToDict(p)).ToDictionary(s => s.Key, s => s.Value)
                    ))
                .ForMember(dvm => dvm.Heading, opt => opt.Ignore())
                .ForMember(dvm => dvm.ControllerName, opt => opt.Ignore());


            CreateMap<ProductFormViewModel, Product>()
                .ForMember(p => p.Supplier, opt => opt.Ignore())
                .ForMember(p => p.ProductCategory, opt => opt.Ignore());

            CreateMap<Product, ProductFormViewModel>()
                .ForMember(pfvm => pfvm.Heading, opt => opt.Ignore())
                .ForMember(pfvm => pfvm.Suppliers, opt => opt.Ignore())
                .ForMember(pfvm => pfvm.ProductCategories, opt => opt.Ignore());

            // ProductCategory
            CreateMap<ProductCategory, CrudRowViewModel>()
                .ForMember(crvm => crvm.Properties, opt => opt.MapFrom(pc =>
                    new Dictionary<string, string> {
                        { "Název", pc.Name }}
                ));

            CreateMap<ProductCategory, DetailViewModel>()
                .ForMember(dvm => dvm.Properties, opt => opt.MapFrom(pc =>
                    new Dictionary<string, string> {
                        { "Název", pc.Name }}
                    .Concat(PropertyDefinitionsToDict(pc)).ToDictionary(s => s.Key, s => s.Value)
                ))
                .ForMember(dvm => dvm.Heading, opt => opt.Ignore())
                .ForMember(dvm => dvm.ControllerName, opt => opt.Ignore());


            CreateMap<ProductCategoryFormViewModel, ProductCategory>()
                .ForMember(pc => pc.Products, opt => opt.Ignore());

            CreateMap<ProductCategory, ProductCategoryFormViewModel>()
                .ForMember(pcfvm => pcfvm.Heading, opt => opt.Ignore());


            // PropertyDefinition
            CreateMap<PropertyDefinitionFormViewModel, PropertyDefinition>()
                .ForMember(pd => pd.ProductCategory, opt => opt.Ignore())
                .ForMember(pd => pd.ProductCategoryId, opt => opt.Ignore())
                .ForMember(pd => pd.PropertyValues, opt => opt.Ignore());

            CreateMap<PropertyDefinition, PropertyDefinitionFormViewModel>();



            // PropertyValue
            CreateMap<PropertyValueFormViewModel, PropertyValue>()
                .ForMember(pv => pv.Id, opt => opt.Ignore())
                .ForMember(pv => pv.PropertyDefinition, opt => opt.Ignore())
                .ForMember(pv => pv.Product, opt => opt.Ignore())
                .ForMember(pv => pv.ProductId, opt => opt.Ignore());

            CreateMap<PropertyValue, PropertyValueFormViewModel>()
                .ForMember(pv => pv.PropertyDefinitionName, opt => opt.MapFrom(pv =>
                   pv.PropertyDefinition.Name))
                .ForMember(pv => pv.MeasureUnit, opt => opt.MapFrom(pv =>
                    pv.PropertyDefinition.MeasureUnit));
        }

        // Product functions

        public IDictionary<string, string> PropertyValuesToDict(Product product)
        {
            return product.PropertyValues.ToDictionary(pv =>
                pv.PropertyDefinition.Name, pv => pv.Value);
        }

        // ProductCategory functions

        public IDictionary<string, string> PropertyDefinitionsToDict(ProductCategory productCategory)
        {
            return productCategory.PropertyDefinitions
                .ToDictionary(dp => dp.Name, dp => dp.MeasureUnit);
        }
    }
}