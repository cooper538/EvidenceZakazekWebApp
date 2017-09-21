using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EvidenceZakazekWebApp.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<PropertyDefinition> PropertyDefinitions { get; set; }

        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            PropertyDefinitions = new Collection<PropertyDefinition>();
            Products = new Collection<Product>();
        }

        public void Modify(ProductCategory updatedProductCategory)
        {
            Name = updatedProductCategory.Name;
            AddOrUpdatePropertyDefinitions(updatedProductCategory.PropertyDefinitions.ToList());
        }

        private void AddOrUpdatePropertyDefinitions(ICollection<PropertyDefinition> updatedPropertyDefinitions)
        {
            foreach (var updatedPropertyDefinition in updatedPropertyDefinitions)
            {
                if (PropertyDefinitions.Any(opd => opd.Id == updatedPropertyDefinition.Id))
                {
                    // Updated
                    var oldPropertyDefinition = PropertyDefinitions.Single(opd => opd.Id == updatedPropertyDefinition.Id);
                    oldPropertyDefinition.Name = updatedPropertyDefinition.Name;
                    oldPropertyDefinition.MeasureUnit = updatedPropertyDefinition.MeasureUnit;
                }
                else
                {
                    // Added
                    PropertyDefinitions.Add(updatedPropertyDefinition);

                    // With new propertyDefinition, have to be add specific productValue to every item in category                       
                    foreach (var product in Products)
                    {
                        updatedPropertyDefinition.PropertyValues.Add(
                            new PropertyValue
                            {
                                ProductId = product.Id,
                                Value = "(Nezadáno)"
                            });
                    }

                }
            }
        }
    }
}