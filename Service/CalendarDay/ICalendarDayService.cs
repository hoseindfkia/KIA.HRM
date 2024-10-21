using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.CalendarDay;

namespace Service.CalendarDay
{
    public interface ICalendarDayService
    {
        Task<Feedback<int>> SetDays(CalenderDayPostViewModel CalenderDay);

        Task<Feedback<CalendarDayByMonthViewModel>> GetByMonth(DateTime dateTime);
    }
}
