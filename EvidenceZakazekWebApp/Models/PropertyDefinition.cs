using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Models
{
    public class PropertyDefinition
    {
        public PropertyDefinition()
        {
            PropertyValues = new Collection<PropertyValue>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string MeasureUnit { get; set; }

        [Required]
        public ProductCategory ProductCategory { get; set; }

        public int ProductCategoryId { get; set; }

        public ICollection<PropertyValue> PropertyValues { get; set; }
    }
}