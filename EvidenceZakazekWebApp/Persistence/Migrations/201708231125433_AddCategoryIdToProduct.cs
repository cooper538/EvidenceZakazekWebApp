namespace EvidenceZakazekWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryIdToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropColumn("dbo.Products", "ProductCategoryId");
        }
    }
}
