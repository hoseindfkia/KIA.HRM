using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    /// <summary>
    /// لیست ماه های سال
    /// </summary>
    public enum MonthType
    {
        [Description("فروردین")]
        Farvardin = 1,
        [Description("اردیبهشت")]
        Ordibehesht = 2,
        [Description("خرداد")]
        Khordad = 3,
        [Description("تیر")]
        Tir = 4,
        [Description("مرداد")]
        Mordad = 5,
        [Description("شهریور")]
        Shahrivar = 6,
        [Description("مهر")]
        Mehr = 7,
        [Description("آبان")]
        Aban = 8,
        [Description("آذر")]
        Azar = 9,
        [Description("دی")]
        Day = 10,
        [Description("بهمن")]
        Bahman = 11,
        [Description("اسفند")]
        Esfand = 12,

    }
}
