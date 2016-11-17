namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _010 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Group", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "Group_GroupId", "dbo.Group");
            DropForeignKey("dbo.Group", "User_UserId", "dbo.User");
            DropIndex("dbo.User", new[] { "Group_GroupId" });
            DropIndex("dbo.Group", new[] { "UserID" });
            DropIndex("dbo.Group", new[] { "User_UserId" });
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.UserId })
                .ForeignKey("dbo.Group", t => t.GroupId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.GroupId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Group", "AdministratorID", c => c.Int(nullable: false));
            DropColumn("dbo.User", "Group_GroupId");
            DropColumn("dbo.Group", "UserID");
            DropColumn("dbo.Group", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Group", "User_UserId", c => c.Int());
            AddColumn("dbo.Group", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Group_GroupId", c => c.Int());
            DropForeignKey("dbo.UserGroup", "UserId", "dbo.User");
            DropForeignKey("dbo.UserGroup", "GroupId", "dbo.Group");
            DropIndex("dbo.UserGroup", new[] { "UserId" });
            DropIndex("dbo.UserGroup", new[] { "GroupId" });
            DropColumn("dbo.Group", "AdministratorID");
            DropTable("dbo.UserGroup");
            CreateIndex("dbo.Group", "User_UserId");
            CreateIndex("dbo.Group", "UserID");
            CreateIndex("dbo.User", "Group_GroupId");
            AddForeignKey("dbo.Group", "User_UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.User", "Group_GroupId", "dbo.Group", "GroupId");
            AddForeignKey("dbo.Group", "UserID", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
