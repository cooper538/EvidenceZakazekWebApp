using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvidenceZakazekWebApp.Repositories
{
    public class ProductCategoryRepository
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

        public ProductCategory GetCategoryWithDefinitionsAndProducts(int id)
        {
            return _context.ProductCategories
                .Include(pc => pc.PropertyDefinitions)
                .Include(pc => pc.Products)
                .SingleOrDefault(pc => pc.Id == id);
        }

        public void Add(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
            return _context.ProductCategories.ToList();
        }
    }
}