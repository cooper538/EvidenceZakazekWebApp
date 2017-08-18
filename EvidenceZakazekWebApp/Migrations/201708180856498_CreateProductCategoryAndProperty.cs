namespace EvidenceZakazekWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductCategoryAndProperty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyDefinitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        MeasureUnit = c.String(nullable: false, maxLength: 255),
                        ProductCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.PropertyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 255),
                        PropertyDefinitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PropertyDefinitions", t => t.PropertyDefinitionId, cascadeDelete: true)
                .Index(t => t.PropertyDefinitionId);
            
            CreateTable(
                "dbo.ProductProperties",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        PropertyDefinitionId = c.Int(nullable: false),
                        PropertyValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.PropertyDefinitionId, t.PropertyValueId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.PropertyDefinitions", t => t.PropertyDefinitionId)
                .ForeignKey("dbo.PropertyValues", t => t.PropertyValueId)
                .Index(t => t.ProductId)
                .Index(t => t.PropertyDefinitionId)
                .Index(t => t.PropertyValueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductProperties", "PropertyValueId", "dbo.PropertyValues");
            DropForeignKey("dbo.ProductProperties", "PropertyDefinitionId", "dbo.PropertyDefinitions");
            DropForeignKey("dbo.ProductProperties", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PropertyValues", "PropertyDefinitionId", "dbo.PropertyDefinitions");
            DropForeignKey("dbo.PropertyDefinitions", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.ProductProperties", new[] { "PropertyValueId" });
            DropIndex("dbo.ProductProperties", new[] { "PropertyDefinitionId" });
            DropIndex("dbo.ProductProperties", new[] { "ProductId" });
            DropIndex("dbo.PropertyValues", new[] { "PropertyDefinitionId" });
            DropIndex("dbo.PropertyDefinitions", new[] { "ProductCategoryId" });
            DropTable("dbo.ProductProperties");
            DropTable("dbo.PropertyValues");
            DropTable("dbo.PropertyDefinitions");
            DropTable("dbo.ProductCategories");
        }
    }
}
