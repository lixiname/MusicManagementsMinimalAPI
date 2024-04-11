namespace MusicManagementsMinimalAPI.Models.DTO
{
    public class UserProfileUpdateDTO:UserProfile
    {
        public string OldPassword { get; set; }
        public string ChangePassword { get; set; }
    }
}
