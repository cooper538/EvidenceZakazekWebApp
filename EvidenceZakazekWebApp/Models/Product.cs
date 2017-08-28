using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string OrderNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string TypeName { get; set; }

        public decimal Price { get; set; }

        public Supplier Supplier { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public ProductCategory ProductCategory { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }

        public ICollection<PropertyValue> PropertyValues { get; set; }
    }
}