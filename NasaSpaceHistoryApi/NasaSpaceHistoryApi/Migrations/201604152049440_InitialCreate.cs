namespace NasaSpaceHistoryApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        AreaName = c.String(),
                        AreaHistory = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        CityHistory = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        CountryHistory = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        HistoryId = c.Int(nullable: false, identity: true),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Summary = c.String(),
                        AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryId)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.AreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Areas", "CityId", "dbo.Cities");
            DropIndex("dbo.Histories", new[] { "AreaId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Areas", new[] { "CityId" });
            DropTable("dbo.Histories");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
        }
    }
}
