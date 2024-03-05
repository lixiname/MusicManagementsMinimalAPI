using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MusicManagementsMinimalAPI.Models
{
    [Table("UserMusicRelate")]
    [Comment("MusicLike information")]
    public class UserMusicRelate
    {
        [Key]
        [Column("MusicId")]
        public int MusicId { get; set; }
        //[Key]
        [Column("UserId")]
        public int UserId { get; set; }
        
    }
}
