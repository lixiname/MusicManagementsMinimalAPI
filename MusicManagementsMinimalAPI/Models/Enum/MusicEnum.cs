using System.ComponentModel;

namespace MusicManagementsMinimalAPI.Models.Enum
{
    public enum MusicReviewEnum
    {
        [Description("待审核")]
        Await = 0,

        [Description("通过")]
        Access = 1,

        [Description("未通过")]
        Reject = 2,
    }
    public enum MusicTypeEnum
    {
        [Description("暂无标签")]
        Type0 = 0,

        [Description("愉快")]
        Type1 = 1,

        [Description("伤感")]
        Type2 = 2,
    }
    
}
