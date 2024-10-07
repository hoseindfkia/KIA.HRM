using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    /// <summary>
    /// نوع ماموریت
    /// </summary>
    public enum MissionType
    {
        /// <summary>
        /// امکان ارتباط با سرور میسر نمی باشد
        /// </summary>
        [Description("ماموریت ساعتی")]
        Hourary = 0,
        /// <summary>
        /// امکان ارتباط با پایگاه داده میسر نمی باشد
        /// </summary>
        [Description("ماموریت روزانه")]
        Daily = 1,
    }
}
