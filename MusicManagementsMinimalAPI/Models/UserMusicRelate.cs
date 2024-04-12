using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MusicManagementsMinimalAPI.Models
{
    [Table("UserMusicRelate")]
    [Comment("MusicCollect information")]
    public class UserMusicRelate
    {
        [Key]
        [Column("MusicId")]
        public long MusicId { get; set; }
        //[Key]
        [Column("UserId")]
        public long UserId { get; set; }
        
    }
}
