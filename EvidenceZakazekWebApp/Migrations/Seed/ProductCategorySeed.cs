using EvidenceZakazekWebApp.Helpers;
using EvidenceZakazekWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EvidenceZakazekWebApp.Migrations.Seed
{
    // Simple classes carrying definitions
    public class SeedProductCategoryDefinition
    {
        public SeedProductCategoryDefinition()
        {
            PropDefAndValDefinitions = new List<SeedPropDefnAndValDefinition>();
        }

        public string CatregoryName { get; set; }
        public string ProductNameBase { get; set; }
        public int ProductsCount { get; set; }
        public List<SeedPropDefnAndValDefinition> PropDefAndValDefinitions { get; set; }
    }

    public class SeedPropDefnAndValDefinition
    {
        public string DefName { get; set; }
        public string MeasureUnit { get; set; }
        public string ValBase { get; set; }
    }

    // Main class
    public static class ProductCategorySeed
    {
        public static void CreateAndPopulateProductCategory(ApplicationDbContext context, SeedProductCategoryDefinition categoryDef, int[] suppliersId)
        {
            // ProductCategory
            context.ProductCategories.AddOrUpdate(
                pc => pc.Name,
                new ProductCategory
                {
                    Name = categoryDef.CatregoryName
                });

            context.SaveChanges();

            // Find out productCategoryId
            var productCategoryId = context.ProductCategories
                .OrderByDescending(pc => pc.Id)
                .FirstOrDefault()
                .Id;

            // PropertyDefinitions
            for (int i = 0; i < categoryDef.PropDefAndValDefinitions.Count; i++)
            {
                context.PropertyDefinitions.AddOrUpdate(
                pd => new { pd.Name },
                new PropertyDefinition
                {
                    Name = categoryDef.PropDefAndValDefinitions[i].DefName,
                    MeasureUnit = categoryDef.PropDefAndValDefinitions[i].MeasureUnit,
                    ProductCategoryId = productCategoryId
                });

                context.SaveChanges();
            }


            // Products
            var rnd = new Random();
            for (int i = 0; i < categoryDef.ProductsCount; i++)
            {
                context.Products.AddOrUpdate(
                    p => new { p.Name },
                    new Product
                    {
                        Name = $"{categoryDef.ProductNameBase} {productCategoryId}{i}",
                        OrderNumber = $"order_{i * 100}",
                        TypeName = $"type_{i * 100 }",
                        Price = new decimal(rnd.Next(1, 1000)),
                        SupplierId = suppliersId.RandomElementUsing(rnd),
                        ProductCategoryId = productCategoryId
                    });
            }

            context.SaveChanges();

            // PropertyValues
            var categoryProducts = context.Products
                .Where(p => p.ProductCategoryId == productCategoryId)
                .ToList();

            var categoryPropertyDefinitions = context.PropertyDefinitions
                .Where(pd => pd.ProductCategoryId == productCategoryId)
                .ToList();

            foreach (var product in categoryProducts)
            {
                foreach (var propertyDefinition in categoryPropertyDefinitions)
                {
                    context.PropertyValues.AddOrUpdate(
                      pv => new { pv.ProductId, pv.PropertyDefinitionId, pv.Value },
                      new PropertyValue
                      {
                          PropertyDefinitionId = propertyDefinition.Id,
                          Value = categoryDef.PropDefAndValDefinitions
                            .FirstOrDefault(pdavd => pdavd.DefName == propertyDefinition.Name).ValBase,
                          ProductId = product.Id
                      });
                }
            }
        }
    }
}