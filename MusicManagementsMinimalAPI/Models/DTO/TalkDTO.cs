using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class TalkDTO
    {
        public long MusicId { get; set; }
        public string MusicName { get; set; }
        public long UploadUserId { get; set; }
        
        public long TalkId { get; set; }
        public string TalkUId { get; set; }
        public string Contents { get; set; }
        public string UserName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public int State { get; set; }
        public TalkDTO() { }
    }
}
