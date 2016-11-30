namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessage",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SenderID = c.Int(nullable: false),
                        ReceiverID = c.Int(nullable: false),
                        Publication_date = c.DateTimeOffset(nullable: false, precision: 7),
                        Message = c.String(nullable: false),
                        From_User_UserId = c.Int(),
                        To_User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.User", t => t.From_User_UserId)
                .ForeignKey("dbo.User", t => t.To_User_UserId)
                .Index(t => t.From_User_UserId)
                .Index(t => t.To_User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatMessage", "To_User_UserId", "dbo.User");
            DropForeignKey("dbo.ChatMessage", "From_User_UserId", "dbo.User");
            DropIndex("dbo.ChatMessage", new[] { "To_User_UserId" });
            DropIndex("dbo.ChatMessage", new[] { "From_User_UserId" });
            DropTable("dbo.ChatMessage");
        }
    }
}
