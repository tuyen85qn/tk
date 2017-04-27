namespace TK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttFileDailySheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailySheets", "FileDailySheet", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailySheets", "FileDailySheet");
        }
    }
}
