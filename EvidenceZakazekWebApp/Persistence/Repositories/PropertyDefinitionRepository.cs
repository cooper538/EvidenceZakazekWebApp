using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvidenceZakazekWebApp.Persistence.Repositories
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

        public void UpdateDefinitionsForProductCategory(int productCategoryId, ICollection<PropertyDefinition> updatedPropertyDefinitions)
        {
            var oldPropertyDefinitions = GetDefinitionsByCategory(productCategoryId);

            var propertyDefinitionsForDelete = oldPropertyDefinitions.Where(oldPd =>
                updatedPropertyDefinitions.Any(updatedPd =>
                    updatedPd.Id != oldPd.Id)).ToList();

            propertyDefinitionsForDelete.ForEach(pd => RemoveWithValues(pd.Id));
        }

        private void RemoveWithValues(int id)
        {
            var definitionForRemove = _context.PropertyDefinitions
                .Include(pd => pd.PropertyValues)
                .SingleOrDefault(pd => pd.Id == id);

            if (definitionForRemove == null)
                throw new ArgumentNullException(nameof(definitionForRemove));

            _context.PropertyValues.RemoveRange(definitionForRemove.PropertyValues);
            _context.PropertyDefinitions.Remove(definitionForRemove);
        }
    }
}