using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class TalkDTO
    {
        public int MusicId { get; set; }
        
        public int UploadUserId { get; set; }
        
        public int TalkId { get; set; }
        
        public string Contents { get; set; }
        public string UserName { get; set; }
        public TalkDTO() { }
    }
}
