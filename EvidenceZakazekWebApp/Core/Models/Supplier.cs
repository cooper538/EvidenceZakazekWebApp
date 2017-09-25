using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.Core.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}