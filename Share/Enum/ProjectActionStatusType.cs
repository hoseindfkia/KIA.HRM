using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    /// <summary>
    /// وضعیت پروژه مشخص می شود
    /// </summary>
    public enum ProjectActionStatusType
    {
        [Description("پروژه باز")]
        /// <summary>
        /// پروژه باز
        /// </summary>
        Open = 0,
        [Description("پروژه بسته شده")]
        /// <summary>
        /// پروژه بسته شده
        /// </summary>
        Close = 1,
        [Description("در انتظار اختصاص به کاربر")]
        /// <summary>
        /// پروژه آماده اختصاص به کاربران می باشد
        /// </summary>
        OpenToAssign = 2,
        [Description("در حال انجام")]
        /// <summary>
        /// پروژه در حال انجام است
        /// </summary>
        InAction = 3,
        [Description("ارجاع")]
        /// <summary>
        /// پروژه به بخش دیگر ارجاع شده است
        /// </summary>
        Send = 4,
        [Description("آرشیو")]
        /// <summary>
        /// پروژه آرشیو شده است
        /// </summary>
        Archive = 5,
        [Description("بازبینی")]
        /// <summary>
        /// نیاز به بازبینی 
        /// </summary>
        ReView = 6,

    }
}
