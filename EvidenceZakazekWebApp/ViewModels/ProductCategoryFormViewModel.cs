using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels.CustomAttributes;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class ProductCategoryFormViewModel
    {

        [Required]
        [DisplayName("Jméno")]
        public string Name { get; set; }

        [EnsureOneElement(ErrorMessage = "Je nutné zadat minimálně 1 vlastnost")] 
        [DisplayName("Vlastnosti produktů v kategorii")]
        public ICollection<PropertyDefinitionFormViewModel> PropertyDefinitions { get; set; }
    }
}