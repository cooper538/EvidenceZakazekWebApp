using EvidenceZakazekWebApp.Controllers;
using EvidenceZakazekWebApp.Models;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class ProductFormViewModel
    {
        public string Heading { get; set; }

        public int Id { get; set; }

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
        public int SupplierId { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }

        [Required]
        [DisplayName("Kategorie")]
        public int ProductCategoryId { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public IEnumerable<PropertyValueFormViewModel> PropertyValues { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ProductsController, ActionResult>> update =
                    (c => c.Update(null));

                Expression<Func<ProductsController, ActionResult>> create =
                    (c => c.Create(null));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}