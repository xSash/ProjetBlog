namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.User", "Group_GroupId", c => c.Int());
            CreateIndex("dbo.User", "Group_GroupId");
            AddForeignKey("dbo.User", "Group_GroupId", "dbo.Group", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Group", "User_UserId", "dbo.User");
            DropForeignKey("dbo.User", "Group_GroupId", "dbo.Group");
            DropForeignKey("dbo.Group", "UserID", "dbo.User");
            DropIndex("dbo.Group", new[] { "User_UserId" });
            DropIndex("dbo.Group", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "Group_GroupId" });
            DropColumn("dbo.User", "Group_GroupId");
            DropTable("dbo.Group");
        }
    }
}
