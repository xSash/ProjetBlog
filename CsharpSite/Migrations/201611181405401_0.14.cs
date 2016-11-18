namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _014 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comment", "User_UserId", "dbo.User");
            DropIndex("dbo.Comment", new[] { "User_UserId" });
            DropColumn("dbo.Comment", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comment", "User_UserId", c => c.Int());
            CreateIndex("dbo.Comment", "User_UserId");
            AddForeignKey("dbo.Comment", "User_UserId", "dbo.User", "UserId");
        }
    }
}
