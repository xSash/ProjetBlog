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
            context.Database.ExecuteSqlCommand( "delete from [ChatMessage]" );

            context.Database.ExecuteSqlCommand( "delete from [Post]" );
            context.Database.ExecuteSqlCommand( "delete from [Group]" );
            context.Database.ExecuteSqlCommand( "delete from [User]" );
            context.Database.ExecuteSqlCommand( "delete from [City]" );
            context.Database.ExecuteSqlCommand( "delete from [Country]" );
            context.Database.ExecuteSqlCommand( "delete from [ReactionType]" );


            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"CommentReaction\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"PostReaction\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Comment\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Post\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Group\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"User\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"ReactionType\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"Country\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"City\", RESEED, 0);" );
            context.Database.ExecuteSqlCommand( " DBCC CHECKIDENT (\"ChatMessage\", RESEED, 0);" );

            context.Countries.AddOrUpdate(
                new Country() { CountryId = 1, Name = "Canada" },
                new Country() { CountryId = 2, Name = "Sweeden" },
                new Country() { CountryId = 3, Name = "Norway" },
                new Country() { CountryId = 4, Name = "Denmark" },
                new Country() { CountryId = 5, Name = "Japan" }
                );
            context.Cities.AddOrUpdate(
                new City() { CityId = 1, Name = "Montreal", CountryID = 1 },
                new City() { CityId = 2, Name = "Stockolm", CountryID = 2 },
                new City() { CityId = 3, Name = "Oslo", CountryID = 3 },
                new City() { CityId = 4, Name = "Copenhagen", CountryID = 4 },
                new City() { CityId = 5, Name = "Kyoto", CountryID = 5 }
                );
            context.SaveChanges();

            context.Users.AddOrUpdate(
                new User() { UserId = 1, Username = "CodeCap_Jeremi", Email = "jeremi@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true, CountryID = 4, CityID = 4, First_name = "Jeremi", Last_name = "Cyr", Gender = 'M', Phone_number = "0000000000", Picture = "1.jpg", Birthday = DateTimeOffset.Now, Description = "i am jeremi" },
                new User() { UserId = 2, Username = "CodeCap_Sasha", Email = "sasha@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = false, CountryID = 1, CityID = 1, First_name = "Sasha", Last_name = "Benjamin", Gender = 'M', Phone_number = "1111111111", Picture = "2.jpg", Birthday = DateTimeOffset.Now, Description = "i am sash" },
                new User() { UserId = 3, Username = "CodeCap_Vincent", Email = "vincent@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = false, CountryID = 5, CityID = 5, First_name = "Vincent", Last_name = "Laferriere", Gender = 'M', Phone_number = "2222222222", Picture = "3.jpg", Birthday = DateTimeOffset.Now, Description = "i am vincent" },
                new User() { UserId = 4, Username = "CodeCap_Adam", Email = "adam@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = false, CountryID = 3, CityID = 3, First_name = "Adam", Last_name = "Cherti", Gender = 'M', Phone_number = "3333333333", Picture = "4.jpg", Birthday = DateTimeOffset.Now, Description = "i am adam" }
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
            
            context.ChatMessages.AddOrUpdate(
                new ChatMessage() { MessageId = 1, SenderID = 1, ReceiverID = 3, Message = "yooo", Publication_date = DateTimeOffset.Parse( "10/10/15 11:36 PM" ) },
                new ChatMessage() { MessageId = 2, SenderID = 3, ReceiverID = 1, Message = "sup", Publication_date = DateTimeOffset.Parse( "10/10/15 11:37 PM" ) },
                new ChatMessage() { MessageId = 3, SenderID = 3, ReceiverID = 1, Message = "wyd", Publication_date = DateTimeOffset.Parse( "10/10/15 11:38 PM" ) },
                new ChatMessage() { MessageId = 4, SenderID = 1, ReceiverID = 3, Message = "coding", Publication_date = DateTimeOffset.Parse( "10/10/15 11:40 PM" ) }
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
                new ReactionType() { ReactionId = 1, Name = "Like", Icon = "fa-thumbs-o-up" },
                new ReactionType() { ReactionId = 2, Name = "Love", Icon = "fa-heartbeat" },
                new ReactionType() { ReactionId = 3, Name = "Cry", Icon = "fa-tint" }
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
