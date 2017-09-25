using EvidenceZakazekWebApp.Core.Repositories;

namespace EvidenceZakazekWebApp.Core
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IProductCategoryRepository ProductCategories { get; }
        ISupplierRepository Suppliers { get; }
        IPropertyValueRepository PropertyValues { get; }
        IPropertyDefinitionRepository PropertyDefinitions { get ;}
        void Complete();
    }
}