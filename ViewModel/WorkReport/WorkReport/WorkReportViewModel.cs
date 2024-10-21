using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Leave;
using ViewModel.WorkReport.Meeting;
using ViewModel.WorkReport.Mission;
using ViewModel.WorkReport.PreparationDocument;

namespace ViewModel.WorkReport.WorkReport
{
    public class WorkReportViewModel
    {
        public IList<LeaveViewModel> Leaves { get; set; }
        public IList<MeetingViewModel> Meetings { get; set; }
        public IList<MissionViewModel> Missions { get; set; }
        public IList<PreparationDocumentViewModel> PreparationDocuments { get; set; }

        public TimeOnly StartWorkTime { get; set; }
        public TimeOnly EndWorkTime { get; set; }

        public long AllTimeWorked { get; set; }
        public string SelectedDayPersian { get; set; }

        public long MorningDurationMinuets { get; set; }
        public long NightDurationMinuets { get; set; }

        public IList<TimeSlotViewModel> TimeSlot { get; set; }

    }
}
