namespace TK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttTblStatistic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statistics", "NationalSecurityCount", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "TrafficAccidentCount", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "CriminalOffenseCount", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "OtherCaseCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statistics", "OtherCaseCount");
            DropColumn("dbo.Statistics", "CriminalOffenseCount");
            DropColumn("dbo.Statistics", "TrafficAccidentCount");
            DropColumn("dbo.Statistics", "NationalSecurityCount");
        }
    }
}
