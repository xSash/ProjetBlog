namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _012 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "User_UserId", c => c.Int());
            CreateIndex("dbo.Comment", "User_UserId");
            AddForeignKey("dbo.Comment", "User_UserId", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "User_UserId", "dbo.User");
            DropIndex("dbo.Comment", new[] { "User_UserId" });
            DropColumn("dbo.Comment", "User_UserId");
        }
    }
}
