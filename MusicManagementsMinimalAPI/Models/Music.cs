using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MusicManagementsMinimalAPI.Models
{
    [Table("Music")]
    [Comment("music information")]
    public class Music
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UploadUserId")]
        public int UploadUserId { get; set; }
        [Column("MusicName")]
        public string Name { get; set; }
        [Column("MusicImageUrl")]
        public string ?MusicImageUrl{ get; set; }
        [Column("Author")]
        public string ?Author {  get; set; }
        [Column("MusicContentUrl")]
        public string ?MusicContentUrl {  get; set; }
        [Column("MusicType")]
        public int ?MusicType { get; set; }
        [Column("Review")]
        public int Review { get; set; }
        [Column("DownLoadNum")]
        public int DownLoadNum { get; set; }
        [Column("AgreedNum")]
        public int AgreedNum { get; set; }
        [Column("TalkNum")]
        public int TalkNum { get; set; }



    }
}
