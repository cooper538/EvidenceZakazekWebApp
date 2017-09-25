namespace EvidenceZakazekWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SetMeasureUnitInPropertyDefinitionOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropertyDefinitions", "MeasureUnit", c => c.String(maxLength: 255));
        }

        public override void Down()
        {
            // Update current records which contain NULL to '0' - solved by https://stackoverflow.com/a/689766/6355668
            Sql("UPDATE [PropertyDefinitions] SET [MeasureUnit]=0 WHERE [MeasureUnit] IS NULL");
            AlterColumn("dbo.PropertyDefinitions", "MeasureUnit", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
