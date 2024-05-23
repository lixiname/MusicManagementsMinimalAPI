using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models
{
    [Table("AgreedRelate")]
    [Comment("AgreedMusic information")]
    public class AgreedRelate
    {
        [Key]
        [Column("MusicId")]
        public long MusicId { get; set; }
        //[Key]
        [Column("UserId")]
        public long UserId { get; set; }
        [Column("time")]
        public DateTime time { get; set; }
    }
}
