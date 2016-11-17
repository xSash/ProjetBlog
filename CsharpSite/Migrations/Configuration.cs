namespace CsharpSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;


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
            
            context.Users.AddOrUpdate(
                new User() { UserId = 0, Username = "CodeCap_Jeremi", Email = "jeremi@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { UserId = 1, Username = "CodeCap_Sasha", Email = "sasha@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { UserId = 2, Username = "CodeCap_Vincent", Email = "vincent@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { UserId = 3, Username = "CodeCap_Adam", Email = "adam@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true }
                );

            context.Posts.AddOrUpdate(
                new Post() { PostId = 1, Title = "I Am God", Contents = "this is a test content post", UserID = 0, Publication_date = DateTimeOffset.Parse("10/10/16 11:36 PM") }
                );

            context.Comments.AddOrUpdate(
                new Models.Comment() { Contents = "Yay", UserID = 1, PostID = 1 }
                );


        }
    }
}
