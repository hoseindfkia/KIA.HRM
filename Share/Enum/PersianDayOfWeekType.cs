using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    public enum PersianDayOfWeekType
    {
        [Description("شنبه")]
        Shanbeh = 0,
        [Description("یکشنبه")]
        YekShanbeh = 1,
        [Description("دوشنبه")]
        DoShanbeh = 2,
        [Description("سه شنبه")]
        SeShanbeh = 3,
        [Description("چهارشنبه")]
        ChaharShanbeh = 4,
        [Description("پنج شنبه")]
        PanjShanbeh = 5,
        [Description("جمعه")]
        Jomee = 6,

    }
}
