using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.ViewModels.Partial
{
    public class PropertyValueFormViewModel
    {
        public int PropertyDefinitionId { get; set; }

        public string PropertyDefinitionName { get; set; }

        public string MeasureUnit { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }
    }
}