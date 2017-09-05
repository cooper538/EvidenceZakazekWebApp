namespace EvidenceZakazekWebApp.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EvidenceZakazekWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EvidenceZakazekWebApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.Suppliers.AddOrUpdate(
                s => s.Name,
                new Supplier { Id = 1, Name = "Sonepar" },
                new Supplier { Id = 2, Name = "Sick" },
                new Supplier { Id = 3, Name = "ControlTech" },
                new Supplier { Id = 4, Name = "Schrack" }
                );

            context.ProductCategories.AddOrUpdate(
                pc => pc.Name,
                new ProductCategory
                {
                    Id = 1,
                    Name = "Jističe",
                },
                new ProductCategory
                {
                    Id = 2,
                    Name = "Stykače",
                });

            context.SaveChanges();

            context.PropertyDefinitions.AddOrUpdate(
                pd => new { pd.Name, pd.ProductCategoryId },
                new PropertyDefinition
                {
                    Id = 1,
                    Name = "Vypínací charakteristika",
                    ProductCategoryId = 1

                },
                new PropertyDefinition
                {
                    Id = 2,
                    Name = "Jmenovitý proud",
                    MeasureUnit = "A",
                    ProductCategoryId = 1
                },
                 new PropertyDefinition
                 {
                     Id = 2,
                     Name = "Počet NO kontaktů",
                     MeasureUnit = "A",
                     ProductCategoryId = 2
                 });

            context.SaveChanges();

            List<int> listNumbers = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                listNumbers.Add(i);
            }

            int categoryId = 1;
            var rnd = new Random();
            listNumbers.ForEach(n => context.Products.AddOrUpdate(
                p => new { p.Name },
                   new Product
                   {
                       Id = n,
                       Name = $"Jistič {categoryId + n}",
                       OrderNumber = $"order_{n * 100}",
                       TypeName = $"type_{ n * 100 }",
                       Price = new decimal(rnd.Next(1,1000)),
                       SupplierId = 1,
                       ProductCategoryId = 1
                   }
                ));
            
            context.SaveChanges();

            listNumbers.ForEach(n => context.PropertyValues.AddOrUpdate(
                pv => new { pv.ProductId, pv.PropertyDefinitionId, pv.Value },
                new PropertyValue
                {
                    ProductId = n,
                    PropertyDefinitionId = 1,
                    Value = "B",
                },
                new PropertyValue
                {
                    ProductId = n,
                    PropertyDefinitionId = 2,
                    Value = $"{n*10}"
                })
            );
        }
    }
}
