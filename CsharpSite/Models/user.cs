namespace CsharpSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    //Region entities
    [Table("User")]
    public partial class User{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Index("ix_uname_pw",IsUnique = true, Order = 1 )]
        [Required, StringLength(32)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Index( "ix_uname_pw", IsUnique = true, Order = 2 )]
        [Required, StringLength(255)]
        public string Email { get; set; }
        [Required]
        public DateTimeOffset Registration_date { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required, StringLength( 64 )]
        public string First_name { get; set; }
        [Required, StringLength( 64 )]
        public string Last_name { get; set; }
        [Required]
        public string Picture { get; set; }

        [Required, ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        [Required, ForeignKey( "City" )]
        public int CityID { get; set; }
        public virtual City City { get; set; }

        [Required]
        public DateTimeOffset Birthday { get; set; }
        [Required, StringLength( 10 )]
        public string Phone_number { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }

        public User() {
            IsAdmin = false;
            Groups = new List<Group>();
            Posts = new List<Post>();
            Followers = new List<User>();
            Following = new List<User>();
        }

        public User(bool isAdmin) {
            IsAdmin = isAdmin;
            Groups = new List<Group>();
            Posts = new List<Post>();
            Followers = new List<User>();
            Following = new List<User>();
        }

        public object Serialize() {
            return new { UserId = this.UserId, Username = this.Username, Email = this.Email, Registration_date = this.Registration_date.ToString() };
        }
    }

    [Table( "Post" )]
    public partial class Post {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Contents { get; set; }
        [Required]
        public DateTimeOffset Publication_date { get; set; }
        [Required, ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostReaction> Reactions { get; set; }

        public Post() {
            Comments = new List<Comment>();
            Reactions = new List<PostReaction>();
        }

        public Post(string title, string contents, int userid) {
            this.Title = title;
            this.Contents = contents;
            this.UserID = userid;
            this.Publication_date = DateTimeOffset.Now;
        }

    }

    [Table( "Comment" )]
    public partial class Comment {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public DateTimeOffset Publication_date { get; set; }

        [Required]
        public int UserID { get; set; }
        [ForeignKey( "UserID" )]
        public virtual User User { get; set; }

        [Required]
        public int PostID { get; set; }
        [ForeignKey( "PostID" )]
        public virtual Post Post { get; set; }

        public virtual ICollection<CommentReaction> Reactions { get; set; }

        public Comment() {
            Reactions = new List<CommentReaction>();
        }

        public Comment( string contents, int userid, int postid ) {
            this.Contents = contents;
            this.UserID = userid;
            this.PostID = postid;
            this.Publication_date = DateTimeOffset.Now;
        }
        public Comment( string contents, int userid, int postid, DateTimeOffset publicationdate ) {
            this.Contents = contents;
            this.UserID = userid;
            this.PostID = postid;
            this.Publication_date = publicationdate;
        }

    }

    [Table("Group")]
    public partial class Group {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int AdministratorID { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public Group() {
            Members = new List<User>();
        }


    }

    [Table("ReactionType")]
    public partial class ReactionType {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Key]
        public int ReactionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }

        public ReactionType() {

        }
    }

    [Table("PostReaction")]
    public partial class PostReaction {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Key]
        public int PostReactionId { get; set; }
        [Required]
        public int ReactionID { get; set; }
        [ForeignKey("ReactionID")]
        public virtual ReactionType Reaction { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [Required]
        public int PostID { get; set; }
        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

        public PostReaction() {

        }

    }

    [Table("CommentReaction")]
    public partial class CommentReaction {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Key]
        public int CommentReactionId { get; set; }
        [Required]
        public int ReactionID { get; set; }
        [ForeignKey( "ReactionID" )]
        public virtual ReactionType Reaction { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey( "UserID" )]
        public virtual User User { get; set; }
        [Required]
        public int CommentID { get; set; }
        [ForeignKey( "CommentID" )]
        public virtual Comment Comment { get; set; }

        public CommentReaction() {

        }

    }

    [Table("Country")]
    public partial class Country {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int CountryId { get; set; }
        [Required, StringLength( 32 )]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<User> Residents { get; set; }

        public Country() {
            Cities = new List<City>();
        }


    }

    [Table("City")]
    public partial class City {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int CityId { get; set; }
        [Required, StringLength( 32 )]
        public string Name { get; set; }
        [Required, ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<User> Residents { get; set; }

        public City() {
            Residents = new List<User>();
        }

    }
    //EndRegion entities




}
