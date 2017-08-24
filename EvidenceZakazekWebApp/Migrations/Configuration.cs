namespace EvidenceZakazekWebApp.Migrations
{
    using Models;
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
                    Name = "Vypínací charakteristika",
                    ProductCategoryId = 1

                },
                new PropertyDefinition
                {
                    Name = "Jmenovitý proud",
                    MeasureUnit = "A",
                    ProductCategoryId = 1
                },
                new PropertyDefinition
                {
                    Name = "Počet NO kontaktů",
                    ProductCategoryId = 2
                });

            context.SaveChanges();

            context.Products.AddOrUpdate(
                p => p.Name,
                new Product
                {
                    Name = "Jistič C4/1",
                    OrderNumber = "BM017104--",
                    TypeName = "BM017104--",
                    Price = 183.70M,
                    SupplierId = 4,
                    ProductCategoryId = 1
                },
                new Product
                {
                    Name = "Motorový spínač s ochranou 0,4-0,63A,2P",
                    OrderNumber = "BE400204--",
                    TypeName = "BE400204--",
                    Price = 421.85M,
                    SupplierId = 4,
                    ProductCategoryId = 1
                },
                new Product
                {
                    Name = "Svorka řadová pro ochranný vodič ST 2,5-PE",
                    OrderNumber = "3031238",
                    TypeName = "ST 2,5-PE",
                    Price = 81.74M,
                    SupplierId = 1,
                    ProductCategoryId = 1
                },
                new Product
                {
                    Name = "Stykač DILER-40 230V 50Hz/240V",
                    OrderNumber = "51759",
                    TypeName = "DILER-40(230V50HZ,240V60HZ)	",
                    Price = 614.22M,
                    SupplierId = 1,
                    ProductCategoryId = 2
                }
            );
        }
    }
}
