using EvidenceZakazekWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class ProductFormViewModel
    {
        [DisplayName("Jméno")]
        public string Name { get; set; }

        [DisplayName("Objednací číslo")]
        public string OrderNumber { get; set; }

        [DisplayName("Typové označení")]
        public string TypeName { get; set; }

        [DisplayName("Cena")]
        public decimal Price { get; set; }

        [DisplayName("Dodavatel")]
        public int Supplier { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}