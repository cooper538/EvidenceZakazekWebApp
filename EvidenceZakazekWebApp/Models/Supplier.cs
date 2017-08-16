using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}