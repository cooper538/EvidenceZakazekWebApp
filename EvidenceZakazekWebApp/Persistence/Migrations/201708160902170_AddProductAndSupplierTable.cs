namespace EvidenceZakazekWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductAndSupplierTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        TypeName = c.String(nullable: false, maxLength: 255),
                        OrderNumber = c.String(nullable: false, maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Supplier_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id, cascadeDelete: true)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Supplier_Id", "dbo.Suppliers");
            DropIndex("dbo.Products", new[] { "Supplier_Id" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Products");
        }
    }
}
