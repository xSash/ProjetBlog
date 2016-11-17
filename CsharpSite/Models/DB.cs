namespace CsharpSite.Models {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB : DbContext {
        public DB()
            : base("name=DB2") {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<UserToGroup> UsersToGroups { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder ) {
            base.OnModelCreating( modelBuilder );
            modelBuilder.Entity<Comment>()
                .HasRequired( c => c.User )
                .WithMany()
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<Comment>()
                .HasRequired( c => c.Post )
                .WithMany(p => p.Comments)
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<Post>()
                .HasRequired( s => s.User );

            modelBuilder.Entity<UserToGroup>()
               .HasRequired( c => c.Group )
               .WithMany()
               .WillCascadeOnDelete( false );
            modelBuilder.Entity<UserToGroup>()
               .HasRequired( c => c.User )
               .WithMany()
               .WillCascadeOnDelete( false );

        }
    }
}
