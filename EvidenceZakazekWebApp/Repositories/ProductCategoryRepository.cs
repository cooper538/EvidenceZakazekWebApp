using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvidenceZakazekWebApp.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductCategory GetCategoryWithDefinitions(int id)
        {
            return _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions)
                .SingleOrDefault(pc => pc.Id == id);
        }

        public ProductCategory GetCategoryWithProductsAndProperties(int id)
        {
            return _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions.Select(pd => pd.PropertyValues))
                .Include(pc => pc.Products)
                .SingleOrDefault(pc => pc.Id == id);
        }

        public void Add(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
        }

        public void RemoveWithProductsWithProperties(ProductCategory productCategory)
        {
            foreach (var propertyDefinition in productCategory.PropertyDefinitions)
            {
                _context.PropertyValues.RemoveRange(propertyDefinition.PropertyValues);
            }

            _context.PropertyDefinitions.RemoveRange(productCategory.PropertyDefinitions);
            _context.Products.RemoveRange(productCategory.Products);

            _context.ProductCategories.Remove(productCategory);
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
            return _context.ProductCategories.ToList();
        }
    }
}