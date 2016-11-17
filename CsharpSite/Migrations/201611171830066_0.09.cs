namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _009 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Contents = c.String(nullable: false),
                        Publication_date = c.DateTimeOffset(nullable: false, precision: 7),
                        UserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Post", t => t.PostID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Contents = c.String(nullable: false),
                        Publication_date = c.DateTimeOffset(nullable: false, precision: 7),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                        Registration_date = c.DateTimeOffset(nullable: false, precision: 7),
                        IsAdmin = c.Boolean(nullable: false),
                        Group_GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Group", t => t.Group_GroupId)
                .Index(t => new { t.Username, t.Email }, unique: true, name: "ix_uname_pw")
                .Index(t => t.Group_GroupId);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        UserID = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.UserID)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "UserID", "dbo.User");
            DropForeignKey("dbo.Comment", "PostID", "dbo.Post");
            DropForeignKey("dbo.Post", "UserID", "dbo.User");
            DropForeignKey("dbo.Group", "User_UserId", "dbo.User");
            DropForeignKey("dbo.User", "Group_GroupId", "dbo.Group");
            DropForeignKey("dbo.Group", "UserID", "dbo.User");
            DropIndex("dbo.Group", new[] { "User_UserId" });
            DropIndex("dbo.Group", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "Group_GroupId" });
            DropIndex("dbo.User", "ix_uname_pw");
            DropIndex("dbo.Post", new[] { "UserID" });
            DropIndex("dbo.Comment", new[] { "PostID" });
            DropIndex("dbo.Comment", new[] { "UserID" });
            DropTable("dbo.Group");
            DropTable("dbo.User");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
        }
    }
}
