using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MusicManagementsMinimalAPI.Models.Enum;
namespace MusicManagementsMinimalAPI.Models
{
    [Table("CommonMusicR")]
    [Comment("music information")]
    public class Music
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("UploadUserId")]
        public long UploadUserId { get; set; }
        [Column("MusicName")]
        public string Name { get; set; }
        [Column("MusicImageUrl")]
        public string ?MusicImageUrl{ get; set; }
        [Column("Author")]
        public string ?Author {  get; set; }
        [Column("MusicContentUrl")]
        public string ?MusicContentUrl {  get; set; }
        [Column("MusicType")]
        public MusicTypeEnum MusicType { get; set; }
        [Column("Review")]
        public MusicReviewEnum Review { get; set; }
        [Column("DownLoadNum")]
        public int DownLoadNum { get; set; }
        [Column("AgreedNum")]
        public int AgreedNum { get; set; }
        [Column("TalkNum")]
        public int TalkNum { get; set; }
        [Column("UsingNum")]
        public int UsingNum { get; set; }
        [Column("CollectNum")]
        public int CollectNum { get; set; }


    }
}
