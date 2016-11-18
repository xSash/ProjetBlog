namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _016 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserUser",
                c => new
                    {
                        FollowerId = c.Int(nullable: false),
                        FollowedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FollowedId })
                .ForeignKey("dbo.User", t => t.FollowerId)
                .ForeignKey("dbo.User", t => t.FollowedId)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUser", "FollowedId", "dbo.User");
            DropForeignKey("dbo.UserUser", "FollowerId", "dbo.User");
            DropIndex("dbo.UserUser", new[] { "FollowedId" });
            DropIndex("dbo.UserUser", new[] { "FollowerId" });
            DropTable("dbo.UserUser");
        }
    }
}
