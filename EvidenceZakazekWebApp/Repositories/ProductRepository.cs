using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvidenceZakazekWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product GetProductWithProperties(int id)
        {
            return _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductCategory)
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .SingleOrDefault(p => p.Id == id);
        }
        
        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void RemoveWithValues(Product product)
        {
            _context.PropertyValues.RemoveRange(product.PropertyValues);

            _context.Products.Remove(product);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductCategory)
                .ToList();
        }


        public IEnumerable<Product> GetProductsWithPropertiesByCategory(int productCategoryId)
        {
            return _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Supplier)
                .Include(p => p.PropertyValues.Select(pv => pv.PropertyDefinition))
                .Where(p => p.ProductCategoryId == productCategoryId)
                .ToList();
        }
    }
}