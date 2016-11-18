namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _015 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentReaction",
                c => new
                    {
                        CommentReactionId = c.Int(nullable: false, identity: true),
                        ReactionID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        CommentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentReactionId)
                .ForeignKey("dbo.Comment", t => t.CommentID)
                .ForeignKey("dbo.ReactionType", t => t.ReactionID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.ReactionID)
                .Index(t => t.UserID)
                .Index(t => t.CommentID);
            
            CreateTable(
                "dbo.PostReaction",
                c => new
                    {
                        PostReactionId = c.Int(nullable: false, identity: true),
                        ReactionID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostReactionId)
                .ForeignKey("dbo.Post", t => t.PostID)
                .ForeignKey("dbo.ReactionType", t => t.ReactionID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.ReactionID)
                .Index(t => t.UserID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.ReactionType",
                c => new
                    {
                        ReactionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Icon = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentReaction", "UserID", "dbo.User");
            DropForeignKey("dbo.CommentReaction", "ReactionID", "dbo.ReactionType");
            DropForeignKey("dbo.CommentReaction", "CommentID", "dbo.Comment");
            DropForeignKey("dbo.PostReaction", "UserID", "dbo.User");
            DropForeignKey("dbo.PostReaction", "ReactionID", "dbo.ReactionType");
            DropForeignKey("dbo.PostReaction", "PostID", "dbo.Post");
            DropIndex("dbo.PostReaction", new[] { "PostID" });
            DropIndex("dbo.PostReaction", new[] { "UserID" });
            DropIndex("dbo.PostReaction", new[] { "ReactionID" });
            DropIndex("dbo.CommentReaction", new[] { "CommentID" });
            DropIndex("dbo.CommentReaction", new[] { "UserID" });
            DropIndex("dbo.CommentReaction", new[] { "ReactionID" });
            DropTable("dbo.ReactionType");
            DropTable("dbo.PostReaction");
            DropTable("dbo.CommentReaction");
        }
    }
}
