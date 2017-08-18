using EvidenceZakazekWebApp.Models;
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

        public IEnumerable<PropertyDefinition> PropertyDefinitions { get; set; }
    }
}