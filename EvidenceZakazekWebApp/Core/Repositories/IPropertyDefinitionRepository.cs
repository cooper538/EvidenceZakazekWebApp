using EvidenceZakazekWebApp.Core.Models;
using System.Collections.Generic;

namespace EvidenceZakazekWebApp.Core.Repositories
{
    public interface IPropertyDefinitionRepository
    {
        IEnumerable<PropertyDefinition> GetDefinitionsByCategory(int categoryId);
        void UpdateDefinitionsForProductCategory(int productCategoryId, ICollection<PropertyDefinition> updatedPropertyDefinitions);
    }
}