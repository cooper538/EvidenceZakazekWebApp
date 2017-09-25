namespace EvidenceZakazekWebApp.Migrations
{
    using Helpers;
    using System;
    using System.Data.Entity.Migrations;

    public partial class PropertyValueProductIdDropDefault : DbMigration
    {
        public override void Up()
        {
            MigrationHelper.DeleteDefaultContraint(this, "PropertyValues", "ProductId");
        }

        public override void Down()
        {
            MigrationHelper.SetDefaultContraint(this, "PropertyValues", "ProductId", "0");
        }
    }
}
