using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.CalendarDay
{
    public class CalendarDayByMonthViewModel
    {
        //public dat MyProperty { get; set; }
        public IList<CalendarDayViewModel> Days { get; set; }
    }
}
