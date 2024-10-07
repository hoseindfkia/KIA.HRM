using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    /// <summary>
    /// نوع مرخصی
    /// </summary>
    public  enum LeaveType
    {
        /// <summary>
        /// امکان ارتباط با سرور میسر نمی باشد
        /// </summary>
        [Description("مرخصی ساعتی")]
        Hourary = 0,
        /// <summary>
        /// امکان ارتباط با پایگاه داده میسر نمی باشد
        /// </summary>
        [Description("مرخصی روزانه")]
        Daily = 1,
         /// <summary>
        /// امکان ارتباط با سرور میسر نمی باشد
        /// </summary>
        [Description("مرخصی استعلاجی")]
        Illness = 2,
        /// <summary>
        /// امکان ارتباط با پایگاه داده میسر نمی باشد
        /// </summary>
        [Description("مرخصی استحقاقی")]
        Statutory = 3,
         /// <summary>
        /// امکان ارتباط با پایگاه داده میسر نمی باشد
        /// </summary>
        [Description("مرخصی تشویقی")]
        Reward = 4,

    }
}
