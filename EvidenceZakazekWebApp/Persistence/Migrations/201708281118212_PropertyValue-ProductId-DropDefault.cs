using EvidenceZakazekWebApp.Extensions;

namespace EvidenceZakazekWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PropertyValueProductIdDropDefault : DbMigration
    {
        public override void Up()
        {
            MigrationExtensions.DeleteDefaultContraint(this, "PropertyValues", "ProductId");
        }

        public override void Down()
        {
            MigrationExtensions.SetDefaultContraint(this, "PropertyValues", "ProductId", "0");
        }
    }
}
