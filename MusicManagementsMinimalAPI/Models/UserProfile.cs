using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models
{
    [Table("CommonUser")]
    [Comment("CommonUser information")]
    public class UserProfile
    {
        [Column("Id")]
        public long Id { get; set; }
        [Column("UserId")]
        public string UserId { get; set; }
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
        [Column("State")]
        public int State { get; set; }
    }
}
