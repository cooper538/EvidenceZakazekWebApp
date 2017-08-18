using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.ViewModels.Partial
{
    public class PropertyDefinitionFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string MeasureUnit { get; set; }
    }
}