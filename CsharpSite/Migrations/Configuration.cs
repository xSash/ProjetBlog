namespace CsharpSite.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
                new User() { Id = 0, Username = "CodeCap_Jeremi", Email = "jeremi@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { Id = 1, Username = "CodeCap_Sasha", Email = "sasha@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { Id = 2, Username = "CodeCap_Vincent", Email = "vincent@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true },
                new User() { Id = 3, Username = "CodeCap_Adam", Email = "adam@codecap.com", Password = "password", Registration_date = DateTimeOffset.Now, IsAdmin = true }
                );

        }
    }
}
