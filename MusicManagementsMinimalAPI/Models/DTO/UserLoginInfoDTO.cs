using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class UserLoginInfoDTO
    {
        
        public string UserId { get; set; }
        public string Password { get; set; }
        public string CaptchaId { get; set; }
        public string CaptchaCode { get; set; }
}
}
