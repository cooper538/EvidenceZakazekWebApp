using System.Collections.Generic;
using EvidenceZakazekWebApp.Models;

namespace EvidenceZakazekWebApp.Repositories
{
    public interface IPropertyDefinitionRepository
    {
        IEnumerable<PropertyDefinition> GetDefinitionsByCategory(int categoryId);
        void RemoveWithValues(int id);
    }
}