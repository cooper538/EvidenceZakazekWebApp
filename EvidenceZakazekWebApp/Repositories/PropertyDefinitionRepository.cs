using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvidenceZakazekWebApp.Repositories
{
    public class PropertyDefinitionRepository : IPropertyDefinitionRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyDefinitionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PropertyDefinition> GetDefinitionsByCategory(int categoryId)
        {
            return _context.PropertyDefinitions
                .Where(pd => pd.ProductCategoryId == categoryId)
                .ToList();
        }

        public void RemoveWithValues(int id)
        {
            var definitionForRemove = _context.PropertyDefinitions
                        .Include(pd => pd.PropertyValues)
                        .SingleOrDefault(pd => pd.Id == id);

            _context.PropertyValues.RemoveRange(definitionForRemove.PropertyValues);
            _context.PropertyDefinitions.Remove(definitionForRemove);
        }
    }
}