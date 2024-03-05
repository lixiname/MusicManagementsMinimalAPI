using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class MusicLikeDTO
    {
        
        public int Id { get; set; }
        public int UploadUserId { get; set; }
        public string Name { get; set; }
        public string? MusicImageUrl { get; set; }
        public string? Author { get; set; }
        public string? MusicContentUrl { get; set; }
        public int? MusicType { get; set; }
        public int UserId { get; set; }
        public MusicLikeDTO() { }
        public MusicLikeDTO(Music music,UserMusicRelate userMusicRelate)
        {
            (Id, UploadUserId, Name, UserId) = (music.Id,music.UploadUserId,music.Name,userMusicRelate.UserId);
        }
    }
}
