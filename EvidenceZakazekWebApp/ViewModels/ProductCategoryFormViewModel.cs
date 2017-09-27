using EvidenceZakazekWebApp.Controllers;
using EvidenceZakazekWebApp.ViewModels.CustomAttributes;
using EvidenceZakazekWebApp.ViewModels.Partial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace EvidenceZakazekWebApp.ViewModels
{
    public class ProductCategoryFormViewModel
    {
        public string Heading { get; set; }

        public int Id { get; set; }

        [Required]
        [DisplayName("Jméno")]
        public string Name { get; set; }

        [EnsureOneElement(ErrorMessage = "Je nutné zadat minimálně 1 vlastnost")]
        [DisplayName("Vlastnosti produktů v kategorii")]
        public IEnumerable<PropertyDefinitionFormViewModel> PropertyDefinitions { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ProductCategoriesController, ActionResult>> update =
                    (c => c.Update(null));

                Expression<Func<ProductCategoriesController, ActionResult>> create =
                    (c => c.Create(null));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}