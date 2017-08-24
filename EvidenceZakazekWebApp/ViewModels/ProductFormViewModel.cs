using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class ProductFormViewModel
    {
        [Required]
        [DisplayName("Jméno")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Objednací číslo")]
        public string OrderNumber { get; set; }

        [Required]
        [DisplayName("Typové označení")]
        public string TypeName { get; set; }

        // Inspired by (\. to \,) https://stackoverflow.com/a/308124
        [RegularExpression(@"^\d+([\,]\d*)?$", ErrorMessage = "Pole Cena musí obsahovat číslo.")]
        [DisplayName("Cena")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Dodavatel")]
        public int Supplier { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }

        [Required]
        [DisplayName("Kategorie")]
        public int Category { get; set; }

        public IEnumerable<ProductCategory> Categories { get; set; }

        public IEnumerable<PropertyValue> PropertyValues { get; set; }
    }
}