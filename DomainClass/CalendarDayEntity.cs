using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    public class CalendarDayEntity: BaseEntity<long>
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Day { get; set; }
        public bool IsHoliday { get; set; }
        [Required]
        [StringLength(Constant.StringLengthName)]
        public string DayPersianName { get; set; }
        public string HolidayName { get; set; }
        /// <summary>
        /// ماه
        /// </summary>
        public MonthType MonthType { get; set; }
        /// <summary>
        /// روز هفته به فارسی
        /// </summary>
        public PersianDayOfWeekType DayOfWeek { get; set; }

        public DateTime Date { get; set; }
    }
}
