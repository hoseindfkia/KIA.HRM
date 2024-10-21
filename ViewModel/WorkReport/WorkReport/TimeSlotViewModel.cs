using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WorkReport.WorkReport
{
    public class TimeSlotViewModel
    {
        public string Title { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public WorkReportType WorkReportType { get; set; }
    }
}
