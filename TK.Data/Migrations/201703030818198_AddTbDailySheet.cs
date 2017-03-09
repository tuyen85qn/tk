namespace TK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTbDailySheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PoliceOrganizations", "Type", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PoliceOrganizations", "Type");
        }
    }
}
