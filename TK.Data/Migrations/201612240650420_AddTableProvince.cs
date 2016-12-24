namespace TK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableProvince : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Situations", name: "SettleBodyID", newName: "PoliceOrganizationID");
            RenameColumn(table: "dbo.Statistics", name: "SettleBodyID", newName: "PoliceOrganizationID");
            RenameIndex(table: "dbo.Situations", name: "IX_SettleBodyID", newName: "IX_PoliceOrganizationID");
            RenameIndex(table: "dbo.Statistics", name: "IX_SettleBodyID", newName: "IX_PoliceOrganizationID");
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Type = c.String(maxLength: 30),
                        ProvinceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Provinces", t => t.ProvinceID, cascadeDelete: true)
                .Index(t => t.ProvinceID);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Type = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Type = c.String(maxLength: 30),
                        ProvinceID = c.Int(nullable: false),
                        DistrictID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Districts", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.DistrictID);
            
            AddColumn("dbo.Situations", "Hamlet", c => c.String(maxLength: 100));
            AddColumn("dbo.Situations", "ProvinceID", c => c.Int(nullable: false));
            AddColumn("dbo.Situations", "DistrictID", c => c.Int());
            AddColumn("dbo.Situations", "WardID", c => c.Int());
            AddColumn("dbo.Statistics", "ProvinceID", c => c.Int(nullable: false));
            AddColumn("dbo.Statistics", "DistrictID", c => c.Int());
            AddColumn("dbo.Statistics", "WardID", c => c.Int());
            CreateIndex("dbo.Situations", "ProvinceID");
            CreateIndex("dbo.Situations", "DistrictID");
            CreateIndex("dbo.Situations", "WardID");
            CreateIndex("dbo.Statistics", "ProvinceID");
            CreateIndex("dbo.Statistics", "DistrictID");
            CreateIndex("dbo.Statistics", "WardID");
            AddForeignKey("dbo.Situations", "DistrictID", "dbo.Districts", "ID");
            AddForeignKey("dbo.Situations", "ProvinceID", "dbo.Provinces", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Situations", "WardID", "dbo.Wards", "ID");
            AddForeignKey("dbo.Statistics", "DistrictID", "dbo.Districts", "ID");
            AddForeignKey("dbo.Statistics", "ProvinceID", "dbo.Provinces", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Statistics", "WardID", "dbo.Wards", "ID");
            DropColumn("dbo.Situations", "Place");
            DropColumn("dbo.Statistics", "Place");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statistics", "Place", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Situations", "Place", c => c.String(nullable: false, maxLength: 256));
            DropForeignKey("dbo.Statistics", "WardID", "dbo.Wards");
            DropForeignKey("dbo.Statistics", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.Statistics", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.Situations", "WardID", "dbo.Wards");
            DropForeignKey("dbo.Wards", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.Situations", "ProvinceID", "dbo.Provinces");
            DropForeignKey("dbo.Situations", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.Districts", "ProvinceID", "dbo.Provinces");
            DropIndex("dbo.Statistics", new[] { "WardID" });
            DropIndex("dbo.Statistics", new[] { "DistrictID" });
            DropIndex("dbo.Statistics", new[] { "ProvinceID" });
            DropIndex("dbo.Wards", new[] { "DistrictID" });
            DropIndex("dbo.Situations", new[] { "WardID" });
            DropIndex("dbo.Situations", new[] { "DistrictID" });
            DropIndex("dbo.Situations", new[] { "ProvinceID" });
            DropIndex("dbo.Districts", new[] { "ProvinceID" });
            DropColumn("dbo.Statistics", "WardID");
            DropColumn("dbo.Statistics", "DistrictID");
            DropColumn("dbo.Statistics", "ProvinceID");
            DropColumn("dbo.Situations", "WardID");
            DropColumn("dbo.Situations", "DistrictID");
            DropColumn("dbo.Situations", "ProvinceID");
            DropColumn("dbo.Situations", "Hamlet");
            DropTable("dbo.Wards");
            DropTable("dbo.Provinces");
            DropTable("dbo.Districts");
            RenameIndex(table: "dbo.Statistics", name: "IX_PoliceOrganizationID", newName: "IX_SettleBodyID");
            RenameIndex(table: "dbo.Situations", name: "IX_PoliceOrganizationID", newName: "IX_SettleBodyID");
            RenameColumn(table: "dbo.Statistics", name: "PoliceOrganizationID", newName: "SettleBodyID");
            RenameColumn(table: "dbo.Situations", name: "PoliceOrganizationID", newName: "SettleBodyID");
        }
    }
}
