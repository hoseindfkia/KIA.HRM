using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
     public enum WorkReportType
    {
        [Description("هیچ")]
        nothing = 0,
        [Description("صبح")]
        morning = 1,
        [Description("شب")]
        night = 2,
        [Description("مرخصی")]
        leave = 3,
        [Description("جلسه")]
        meeting = 4,
        [Description("ماموریت")]
        mission = 5,
        [Description("تهیه مدرک")]
        preparationDocument = 6,
    }
}
