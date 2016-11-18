namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<CsharpSite.Models.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CsharpSite.Models.DB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Database.ExecuteSqlCommand( "delete from [UserGroup]" );
            context.Database.ExecuteSqlCommand( "delete from [UserUser]" );
            context.Database.ExecuteSqlCommand( "delete from [CommentReaction]" );
            context.Database.ExecuteSqlCommand( "delete from [PostReaction]" );
            context.Database.ExecuteSqlCommand( "delete from [Comment]" );
            context.Database.ExecuteSqlCommand( "delete from [Post]" );
            context.Database.ExecuteSqlCommand( "delete from [Group]" );
            context.Database.ExecuteSqlCommand( "delete from [User]" );
            context.Database.ExecuteSqlCommand( "delete from [ReactionType]" );

            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"CommentReaction\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"PostReaction\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Comment\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Post\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Group\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"User\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"ReactionType\", RESEED, 0);" );

            context.Users.AddOrUpdate(
                new User() { UserId = 1, Username = "CodeCap_Jeremi", Email = "jeremi@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { UserId = 2, Username = "CodeCap_Sasha", Email = "sasha@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { UserId = 3, Username = "CodeCap_Vincent", Email = "vincent@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { UserId = 4, Username = "CodeCap_Adam", Email = "adam@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true }
                );
            context.SaveChanges();

            context.Posts.AddOrUpdate(
                new Post() { PostId = 1, Title = "I Am God", Contents = "this is a test content post", UserID = 1, Publication_date = DateTimeOffset.Parse("10/10/15 11:36 PM") },
                new Post() { PostId = 2, Title = "Seriously...", Contents = "yeah, well fuck you too!", UserID = 2, Publication_date = DateTimeOffset.Parse( "03/05/16 14:37 PM" ) },
                new Post() { PostId = 3, Title = "Yay", Contents = "this is a test content post", UserID = 3, Publication_date = DateTimeOffset.Parse( "10/10/15 11:36 PM" ) },
                new Post() { PostId = 4, Title = "ohhh", Contents = "this is another test content post", UserID = 3, Publication_date = DateTimeOffset.Parse( "10/10/15 11:36 PM" ) },
                new Post() { PostId = 5, Title = "So funny!", Contents = "Lorem ipsum", UserID = 3, Publication_date = DateTimeOffset.Parse( "10/10/15 11:36 PM" ) }

                );
            context.SaveChanges();

            context.Comments.AddOrUpdate(
                new Models.Comment() { Contents = "Yay", UserID = 2, PostID = 1 },
                new Models.Comment() { Contents = "So lame...", UserID = 3, PostID = 1 },
                new Models.Comment() { Contents = "y u do this??", UserID = 1, PostID = 2 },
                new Models.Comment() { Contents = "y u do this??", UserID = 4, PostID = 3 },
                new Models.Comment() { Contents = "y u do this??", UserID = 3, PostID = 2 },
                new Models.Comment() { Contents = "y u do this??", UserID = 2, PostID = 4 },
                new Models.Comment() { Contents = "pouf", UserID = 4, PostID = 2 },
                new Models.Comment() { Contents = "pouf", UserID = 1, PostID = 3 },
                new Models.Comment() { Contents = "pouf", UserID = 2, PostID = 2 },
                new Models.Comment() { Contents = "pouf", UserID = 3, PostID = 4 }
                );

            context.Groups.AddOrUpdate(
                new Group() { GroupId = 1, Name = "teamahuntsic", Description = "The original group. The one to rule them all.", AdministratorID = 1 }
                );
            context.SaveChanges();

            context.ReactionTypes.AddOrUpdate(
                new ReactionType() { ReactionId = 1, Name = "Like", Icon = "likeicon.png" },
                new ReactionType() { ReactionId = 2, Name = "Love", Icon = "loveicon.png" },
                new ReactionType() { ReactionId = 3, Name = "Cry", Icon = "cryicon.png" }
                );
            context.SaveChanges();

            context.PostReactions.AddOrUpdate(
                new PostReaction() { PostReactionId = 1, ReactionID = 1, PostID = 1, UserID = 1 },
                new PostReaction() { PostReactionId = 2, ReactionID = 1, PostID = 1, UserID = 2 },
                new PostReaction() { PostReactionId = 3, ReactionID = 3, PostID = 1, UserID = 3 }
                );
            context.SaveChanges();

            context.Users.First().Followers.Add( context.Users.ToArray()[1] );
            context.Users.First().Following.Add( context.Users.ToArray()[1] );
            context.Users.First().Following.Add( context.Users.ToArray()[2] );
            context.Users.First().Following.Add( context.Users.ToArray()[3] );

            context.SaveChanges();

        }
    }
}
