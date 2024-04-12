using System.ComponentModel;

namespace MusicManagementsMinimalAPI.Models.Enum
{
    public class UserEnum
    {
    }

    public enum UserStateEnum
    {
        [Description("禁用")]
        InValid = 0,

        [Description("正常")]
        Valid = 1, 
    }
}
