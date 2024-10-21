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
        /// امرخصی ساعتی
        /// </summary>
        [Description("مرخصی ساعتی")]
        Hourary = 0,
        /// <summary>
        /// مرخصی روزانه
        /// </summary>
        [Description("مرخصی روزانه")]
        Daily = 1,
        /// <summary>
        /// مرخصی استعلاجی
        /// </summary>
        [Description("مرخصی استعلاجی")]
        Illness = 2,
        /// <summary>
        /// مرخصی استحقاقی
        /// </summary>
        [Description("مرخصی استحقاقی")]
        Statutory = 3,
        /// <summary>
        /// مرخصی تشویقی
        /// </summary>
        [Description("مرخصی تشویقی")]
        Reward = 4,

    }
}
