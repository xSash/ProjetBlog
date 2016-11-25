namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Description");
        }
    }
}
