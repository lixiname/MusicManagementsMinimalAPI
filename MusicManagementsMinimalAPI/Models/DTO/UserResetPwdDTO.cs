namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class UserResetPwdDTO
    {
        public string UserId { get; set; }
        public string EmailRandomId { get; set; }
        public string EmailCode { get; set; }
        public string ChangePassword { get; set; }
    }
}
