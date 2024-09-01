using System.ComponentModel;

namespace Share.Enum
{
    /// <summary>
    /// نوع پیغام جهت نحوه نمایش در سمت رابط کاربری
    /// </summary>
    public enum MessageType
    {

        [Description("اطلاع رسانی")]
        Info = 0,

        [Description("هشدار")]
        Warninig = 1,

        [Description("خطا")]
        Error = 2,

        [Description("در حال انتظار")]
        Waiting = 3
    }
}