namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _007 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Group", "User_UserId", "dbo.User");
            DropIndex("dbo.User", new[] { "Group_GroupId" });
            DropIndex("dbo.Group", new[] { "User_UserId" });
            CreateTable(
                "dbo.UserToGroup",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        Group_GroupId = c.Int(),
                        Group_GroupId1 = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                        User_UserId1 = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserID, t.GroupID })
                .ForeignKey("dbo.Group", t => t.Group_GroupId1)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .ForeignKey("dbo.User", t => t.User_UserId1)
                .Index(t => t.Group_GroupId)
                .Index(t => t.Group_GroupId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
            //DropColumn("dbo.User", "Group_GroupId");
            //DropColumn("dbo.Group", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Group", "User_UserId", c => c.Int());
            AddColumn("dbo.User", "Group_GroupId", c => c.Int());
            DropForeignKey("dbo.UserToGroup", "User_UserId1", "dbo.User");
            DropForeignKey("dbo.UserToGroup", "User_UserId", "dbo.User");
            DropForeignKey("dbo.UserToGroup", "Group_GroupId1", "dbo.Group");
            DropIndex("dbo.UserToGroup", new[] { "User_UserId1" });
            DropIndex("dbo.UserToGroup", new[] { "User_UserId" });
            DropIndex("dbo.UserToGroup", new[] { "Group_GroupId1" });
            DropIndex("dbo.UserToGroup", new[] { "Group_GroupId" });
            DropTable("dbo.UserToGroup");
            CreateIndex("dbo.Group", "User_UserId");
            CreateIndex("dbo.User", "Group_GroupId");
            AddForeignKey("dbo.Group", "User_UserId", "dbo.User", "UserId");
        }
    }
}
