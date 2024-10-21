using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.CalendarDay
{
    public class CalendarDayViewModel
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool IsHoliday { get; set; }
        public string DayPersianName { get; set; }
        public string HolidayName { get; set; }
        public string MonthName { get; set; }
        public MonthType MonthType { get; set; }
        public DateTime Date { get; set; }
        public PersianDayOfWeekType DayOfWeek { get; set; }
    }
}
