using System.Data.Entity.Migrations;
using EvidenceZakazekWebApp.Extensions;

namespace EvidenceZakazekWebApp.Migrations
{
    public partial class ProductProductCategoryIdDropDefault : DbMigration
    {
        public override void Up()
        {
            MigrationExtensions.DeleteDefaultContraint(this, "Products", "ProductCategoryId");
        }

        public override void Down()
        {
            MigrationExtensions.SetDefaultContraint(this, "Products", "ProductCategoryId", "0");
        }
    }
}
