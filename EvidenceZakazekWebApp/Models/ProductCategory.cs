using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<PropertyDefinition> PropertyDefinitions { get; set; }

        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            PropertyDefinitions = new Collection<PropertyDefinition>();
            Products = new Collection<Product>();
        }
    }
}