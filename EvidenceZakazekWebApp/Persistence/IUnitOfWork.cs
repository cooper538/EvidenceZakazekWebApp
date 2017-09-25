using EvidenceZakazekWebApp.Repositories;

namespace EvidenceZakazekWebApp.Persistence
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