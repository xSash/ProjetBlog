namespace CsharpSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
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

        //public virtual ICollection<Post> Posts { get; set; }
        //public virtual ICollection<Comment> Comments { get; set; }

        public User() {
            IsAdmin = false;
        }

        public User(bool isAdmin) {
            IsAdmin = isAdmin;
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


        public Post() {
            Comments = new List<Comment>();
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


        public Comment() {

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

    }
}
