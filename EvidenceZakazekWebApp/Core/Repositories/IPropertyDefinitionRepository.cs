using System.Collections.Generic;
using EvidenceZakazekWebApp.Core.Models;

namespace EvidenceZakazekWebApp.Core.Repositories
{
    public interface IPropertyDefinitionRepository
    {
        IEnumerable<PropertyDefinition> GetDefinitionsByCategory(int categoryId);
        void RemoveWithValues(int id);
    }
}