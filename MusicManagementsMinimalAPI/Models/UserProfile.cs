using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models
{
    [Table("UserS")]
    [Comment("Users information")]
    public class UserProfile
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string ?Name { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("profilePictureUrl")]
        public string? ProfilePictureUrl { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("Phone")]
        public string? Phone { get; set; }
    }
}
