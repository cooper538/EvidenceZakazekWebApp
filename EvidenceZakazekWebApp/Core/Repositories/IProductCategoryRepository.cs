using System.Collections.Generic;
using EvidenceZakazekWebApp.Core.Models;

namespace EvidenceZakazekWebApp.Core.Repositories
{
    public interface IProductCategoryRepository
    {
        ProductCategory GetCategoryWithDefinitions(int id);
        ProductCategory GetCategoryWithProductsAndProperties(int id);
        void Add(ProductCategory productCategory);
        void RemoveWithProductsWithProperties(ProductCategory productCategory);
        IEnumerable<ProductCategory> GetCategories();
    }
}