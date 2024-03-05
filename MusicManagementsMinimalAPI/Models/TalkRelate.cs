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
        public int MusicId { get; set; }
        [Column("UploadUserId")]
        public int UploadUserId{ get; set; }
        [Column("TalkId")]
        public int TalkId { get; set; }
        [Column("Contents")]
        public string Contents { get; set; }
    }
}
