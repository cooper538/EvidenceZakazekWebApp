namespace EvidenceZakazekWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameKeyForeignPropertyInProduct : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "Supplier_Id", newName: "SupplierId");
            RenameIndex(table: "dbo.Products", name: "IX_Supplier_Id", newName: "IX_SupplierId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_SupplierId", newName: "IX_Supplier_Id");
            RenameColumn(table: "dbo.Products", name: "SupplierId", newName: "Supplier_Id");
        }
    }
}
