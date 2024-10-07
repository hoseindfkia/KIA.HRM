using System.ComponentModel;

namespace Share.Enum
{
    /// <summary>
    /// این نوع شمارشی مشخص می کند عکس ها و فایل ها 
    /// از چه فرمی هستند تا در پوشه مربوطه خود ذخیره گردد
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// پروژه
        /// </summary>
        [Description("پروژه")]
        Project = 0,

         /// <summary>
        /// ماموریت
        /// </summary>
        [Description("ماموریت")]
        Mission = 1,

         /// <summary>
        /// جلسه
        /// </summary>
        [Description("جلسه")]
        Meeting = 2,



       
    }
}
