using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Core.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public PropertyDefinition PropertyDefinition { get; set; }

        [Required]
        public int PropertyDefinitionId { get; set; }

        public Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}