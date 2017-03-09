namespace TK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTbStatistic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statistics", "TotalSituationCount", c => c.Int(nullable: false));
            DropColumn("dbo.Statistics", "NationalSecurityCount");
            DropColumn("dbo.Statistics", "TrafficAccidentCount");
            DropColumn("dbo.Statistics", "CriminalOffenseCount");
            DropColumn("dbo.Statistics", "OtherCaseCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statistics", "OtherCaseCount", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "CriminalOffenseCount", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "TrafficAccidentCount", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "NationalSecurityCount", c => c.Int(nullable: false));
            DropColumn("dbo.Statistics", "TotalSituationCount");
        }
    }
}
