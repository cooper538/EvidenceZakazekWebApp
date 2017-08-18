using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        [Required]
        public PropertyDefinition PropertyDefinition { get; set; }

        public int PropertyDefinitionId { get; set; }
    }
}