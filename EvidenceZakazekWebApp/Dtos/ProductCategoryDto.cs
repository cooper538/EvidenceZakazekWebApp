using System.Collections.Generic;

namespace EvidenceZakazekWebApp.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PropertyDefinitionDto> PropertyDefinitions { get; set; }
    }
}