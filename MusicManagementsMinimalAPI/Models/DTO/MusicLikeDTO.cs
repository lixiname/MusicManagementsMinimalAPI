using MusicManagementsMinimalAPI.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class MusicLikeDTO
    {
        
        public long Id { get; set; }
        public long UploadUserId { get; set; }
        public string Name { get; set; }
        public string? MusicImageUrl { get; set; }
        public string? Author { get; set; }
        public string? MusicContentUrl { get; set; }
        public MusicTypeEnum MusicType { get; set; }
        public long UserId { get; set; }
        public int DownLoadNum { get; set; }
        public int AgreedNum { get; set; }
        public int TalkNum { get; set; }
        public int UsingNum { get; set; }
        public int CollectNum { get; set; }

        public MusicLikeDTO() { }
        public MusicLikeDTO(Music music,UserMusicRelate userMusicRelate)
        {
            (Id, UploadUserId, Name, UserId, DownLoadNum, AgreedNum, TalkNum, UsingNum, CollectNum) = 
                (music.Id,music.UploadUserId,music.Name,userMusicRelate.UserId, music.DownLoadNum, music.AgreedNum, music.TalkNum,
                music.UsingNum, music.CollectNum);
        }
    }
}
