namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _021 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatMessage", "From_User_UserId", "dbo.User");
            DropForeignKey("dbo.ChatMessage", "To_User_UserId", "dbo.User");
            DropIndex("dbo.ChatMessage", new[] { "From_User_UserId" });
            DropIndex("dbo.ChatMessage", new[] { "To_User_UserId" });
            AddColumn("dbo.ChatMessage", "Seen", c => c.Boolean(nullable: false));
            DropColumn("dbo.ChatMessage", "From_User_UserId");
            DropColumn("dbo.ChatMessage", "To_User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatMessage", "To_User_UserId", c => c.Int());
            AddColumn("dbo.ChatMessage", "From_User_UserId", c => c.Int());
            DropColumn("dbo.ChatMessage", "Seen");
            CreateIndex("dbo.ChatMessage", "To_User_UserId");
            CreateIndex("dbo.ChatMessage", "From_User_UserId");
            AddForeignKey("dbo.ChatMessage", "To_User_UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.ChatMessage", "From_User_UserId", "dbo.User", "UserId");
        }
    }
}
