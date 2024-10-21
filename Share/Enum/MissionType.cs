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
        /// ماموریت ساعتی
        /// </summary>
        [Description("ماموریت ساعتی")]
        Hourary = 0,
        /// <summary>
        /// ماموریت روزانه
        /// </summary>
        [Description("ماموریت روزانه")]
        Daily = 1,
    }
}
