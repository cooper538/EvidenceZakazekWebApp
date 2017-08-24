namespace EvidenceZakazekWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetMeasureUnitInPropertyDefinitionOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropertyDefinitions", "MeasureUnit", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropertyDefinitions", "MeasureUnit", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
