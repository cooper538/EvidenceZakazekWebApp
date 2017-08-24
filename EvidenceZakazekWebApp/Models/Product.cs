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

        [Required]
        public Supplier Supplier { get; set; }

        public int SupplierId { get; set; }

        [Required]
        public ProductCategory ProductCategory { get; set; }

        public int ProductCategoryId { get; set; }
    }
}