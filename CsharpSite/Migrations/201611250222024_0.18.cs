namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Country", t => t.CountryID)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.CountryId);
            
            AddColumn("dbo.User", "First_name", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.User", "Last_name", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.User", "Picture", c => c.String(nullable: false));
            AddColumn("dbo.User", "CountryID", c => c.Int(nullable: false));
            AddColumn("dbo.User", "CityID", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Birthday", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.User", "Phone_number", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.User", "CountryID");
            CreateIndex("dbo.User", "CityID");
            AddForeignKey("dbo.User", "CityID", "dbo.City", "CityId");
            AddForeignKey("dbo.User", "CountryID", "dbo.Country", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.City", "CountryID", "dbo.Country");
            DropForeignKey("dbo.User", "CountryID", "dbo.Country");
            DropForeignKey("dbo.User", "CityID", "dbo.City");
            DropIndex("dbo.User", new[] { "CityID" });
            DropIndex("dbo.User", new[] { "CountryID" });
            DropIndex("dbo.City", new[] { "CountryID" });
            DropColumn("dbo.User", "Phone_number");
            DropColumn("dbo.User", "Birthday");
            DropColumn("dbo.User", "CityID");
            DropColumn("dbo.User", "CountryID");
            DropColumn("dbo.User", "Picture");
            DropColumn("dbo.User", "Last_name");
            DropColumn("dbo.User", "First_name");
            DropTable("dbo.Country");
            DropTable("dbo.City");
        }
    }
}
