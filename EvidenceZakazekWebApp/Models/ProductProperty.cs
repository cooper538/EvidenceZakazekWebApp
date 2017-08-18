using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvidenceZakazekWebApp.Models
{
    public class ProductProperty
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int PropertyDefinitionId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int PropertyValueId { get; set; }

        public Product Product { get; set; }

        public PropertyDefinition PropertyDefinition { get; set; }

        public PropertyValue PropertyValue { get; set; }
    }
}