using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models
{
    [Table("TalkRelate")]
    [Comment("music information")]
    public class TalkRelate
    {
        //error的主码会不会导致删除多个，如何EF去设置联合主码
        [Key]
        [Column("MusicId")]
        public long MusicId { get; set; }
        [Column("UploadUserId")]
        public long UploadUserId { get; set; }
        [Column("TalkId")]
        public long TalkId { get; set; }
        [Column("Contents")]
        public string Contents { get; set; }
        [Column("time")]
        public DateTime time { get; set; }
        [Column("AgreedNum")]
        public int AgreedNum { get; set; }
    }
}
