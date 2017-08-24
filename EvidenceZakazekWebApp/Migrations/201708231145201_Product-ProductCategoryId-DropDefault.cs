using EvidenceZakazekWebApp.Helpers;
using System.Data.Entity.Migrations;

namespace EvidenceZakazekWebApp.Migrations
{
    public partial class ProductProductCategoryIdDropDefault : DbMigration
    {
        public override void Up()
        {
            MigrationHelper.DeleteDefaultContraint(this, "Products", "ProductCategoryId");
        }

        public override void Down()
        {
            MigrationHelper.SetDefaultContraint(this, "Products", "ProductCategoryId", "0");
        }
    }
}
