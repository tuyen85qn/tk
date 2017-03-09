namespace TK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTbTypeReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailySheets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayReport = c.DateTime(nullable: false),
                        PoliceOrganizationID = c.Int(nullable: false),
                        DirectCommand = c.String(maxLength: 20),
                        OnDuty = c.String(maxLength: 20),
                        TypeReportID = c.Int(),
                        Description = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PoliceOrganizations", t => t.PoliceOrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.TypeReports", t => t.TypeReportID)
                .Index(t => t.PoliceOrganizationID)
                .Index(t => t.TypeReportID);
            
            CreateTable(
                "dbo.TypeReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailySheets", "TypeReportID", "dbo.TypeReports");
            DropForeignKey("dbo.DailySheets", "PoliceOrganizationID", "dbo.PoliceOrganizations");
            DropIndex("dbo.DailySheets", new[] { "TypeReportID" });
            DropIndex("dbo.DailySheets", new[] { "PoliceOrganizationID" });
            DropTable("dbo.TypeReports");
            DropTable("dbo.DailySheets");
        }
    }
}
