namespace EvidenceZakazekWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductPropertyTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductProperties", "PropertyDefinitionId", "dbo.PropertyDefinitions");
            DropForeignKey("dbo.ProductProperties", "PropertyValueId", "dbo.PropertyValues");
            DropIndex("dbo.ProductProperties", new[] { "ProductId" });
            DropIndex("dbo.ProductProperties", new[] { "PropertyDefinitionId" });
            DropIndex("dbo.ProductProperties", new[] { "PropertyValueId" });
            AddColumn("dbo.PropertyValues", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.PropertyValues", "ProductId");
            AddForeignKey("dbo.PropertyValues", "ProductId", "dbo.Products", "Id");
            DropTable("dbo.ProductProperties");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductProperties",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        PropertyDefinitionId = c.Int(nullable: false),
                        PropertyValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.PropertyDefinitionId, t.PropertyValueId });
            
            DropForeignKey("dbo.PropertyValues", "ProductId", "dbo.Products");
            DropIndex("dbo.PropertyValues", new[] { "ProductId" });
            DropColumn("dbo.PropertyValues", "ProductId");
            CreateIndex("dbo.ProductProperties", "PropertyValueId");
            CreateIndex("dbo.ProductProperties", "PropertyDefinitionId");
            CreateIndex("dbo.ProductProperties", "ProductId");
            AddForeignKey("dbo.ProductProperties", "PropertyValueId", "dbo.PropertyValues", "Id");
            AddForeignKey("dbo.ProductProperties", "PropertyDefinitionId", "dbo.PropertyDefinitions", "Id");
            AddForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
