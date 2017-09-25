using System.Collections.Generic;
using EvidenceZakazekWebApp.Models;

namespace EvidenceZakazekWebApp.Repositories
{
    public interface IProductRepository
    {
        Product GetProductWithProperties(int id);
        void Add(Product product);
        void RemoveWithValues(Product product);
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsWithPropertiesByCategory(int productCategoryId);
    }
}