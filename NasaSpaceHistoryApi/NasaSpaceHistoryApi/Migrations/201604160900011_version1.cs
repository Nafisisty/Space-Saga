namespace NasaSpaceHistoryApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Histories", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Histories", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Histories", "Longitude", c => c.String());
            AlterColumn("dbo.Histories", "Latitude", c => c.String());
        }
    }
}
