using EvidenceZakazekWebApp.Core.Models;
using EvidenceZakazekWebApp.Persistence;

namespace EvidenceZakazekWebApp.Migrations
{
    using Seed;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Persistence\Migrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Launching inside try-catch block to see validationErrors if occur
            try
            {
                // Suppliers
                context.Suppliers.AddOrUpdate(
                    s => s.Name,
                    new Supplier { Id = 1, Name = "Sonepar" },
                    new Supplier { Id = 2, Name = "Sick" },
                    new Supplier { Id = 3, Name = "ControlTech" },
                    new Supplier { Id = 4, Name = "Schrack" }
                    );

                // Product Categories
                var seedProductCategoryDefinitions1 = new SeedProductCategoryDefinition
                {
                    CategoryName = "Jističe",
                    ProductNameBase = "Jistič",
                    ProductsCount = 10,
                    PropDefAndValDefinitions = new List<SeedPropDefnAndValDefinition>
                    {
                        new SeedPropDefnAndValDefinition
                        {
                            DefName = "Jmenovitý proud",
                            MeasureUnit = "A",
                            ValBase = "10"
                        },
                        new SeedPropDefnAndValDefinition
                        {
                            DefName = "Počet fází",
                            MeasureUnit = null,
                            ValBase = "fáze"
                        }
                    }
                };

                ProductCategorySeed.CreateAndPopulateProductCategory(context, seedProductCategoryDefinitions1, new int[] { 1, 2, 3, 4 });

                context.SaveChanges();

                var seedProductCategoryDefinitions2 = new SeedProductCategoryDefinition
                {
                    CategoryName = "Stykače",
                    ProductNameBase = "Relé",
                    ProductsCount = 10,
                    PropDefAndValDefinitions = new List<SeedPropDefnAndValDefinition>
                    {
                        new SeedPropDefnAndValDefinition
                        {
                            DefName = "Počet NO kontaktů",
                            MeasureUnit = null,
                            ValBase = "2"
                        }
                    }
                };

                ProductCategorySeed.CreateAndPopulateProductCategory(context, seedProductCategoryDefinitions2, new int[] { 1, 2, 3, 4 });

                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }
    }
}
