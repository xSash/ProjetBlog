namespace CsharpSite.Models {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class DB : DbContext {
        public DB()
            : base("name=DB2") {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<ReactionType> ReactionTypes { get; set; }
        public virtual DbSet<PostReaction> PostReactions { get; set; }
        public virtual DbSet<CommentReaction> CommentReactions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        

        protected override void OnModelCreating( DbModelBuilder modelBuilder ) {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating( modelBuilder );
            modelBuilder.Entity<Comment>()
                .HasRequired( c => c.User )
                .WithMany()
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<Comment>()
                .HasRequired( c => c.Post )
                .WithMany(p => p.Comments)
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<City>()
                .HasRequired( c => c.Country )
                .WithMany( p => p.Cities )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<User>()
                .HasRequired( c => c.City )
                .WithMany( p => p.Residents )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<User>()
               .HasRequired( c => c.Country )
               .WithMany( p => p.Residents )
               .WillCascadeOnDelete( false );

            modelBuilder.Entity<Post>()
                .HasRequired( s => s.User );

            modelBuilder.Entity<PostReaction>()
                .HasRequired( c => c.Post )
                .WithMany( p => p.Reactions )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<PostReaction>()
                .HasRequired( c => c.User )
                .WithMany()
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<CommentReaction>()
                .HasRequired( c => c.Comment )
                .WithMany( p => p.Reactions )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<CommentReaction>()
                .HasRequired( c => c.User )
                .WithMany()
                .WillCascadeOnDelete( false );

           

            modelBuilder.Entity<Group>()
             .HasMany<User>( x => x.Members )
             .WithMany( x => x.Groups )
             .Map( x => {
                 x.MapLeftKey( "GroupId" );
                 x.MapRightKey( "UserId" );
                 x.ToTable( "UserGroup" );
             } );

            modelBuilder.Entity<User>()
             .HasMany<User>( x => x.Following )
             .WithMany( x => x.Followers )
             .Map( x => {
                 x.MapLeftKey( "FollowerId" );
                 x.MapRightKey( "FollowedId" );
                 x.ToTable( "UserUser" );
             } );


        }
    }
}
