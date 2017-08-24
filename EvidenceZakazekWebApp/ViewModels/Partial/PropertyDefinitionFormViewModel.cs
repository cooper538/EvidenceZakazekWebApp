using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.ViewModels.Partial
{
    public class PropertyDefinitionFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Jméno")]
        public string Name { get; set; }

        [StringLength(255)]
        [DisplayName("Měrná Jednotka")]
        public string MeasureUnit { get; set; }
    }
}