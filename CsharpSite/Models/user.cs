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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(32)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public DateTimeOffset Registration_date { get; set; }

        [Required]
        public bool IsAdmin { get; set; }


        public User() {
            IsAdmin = false;
        }

        public User(bool isAdmin) {
            IsAdmin = isAdmin;
        }


    }
}
