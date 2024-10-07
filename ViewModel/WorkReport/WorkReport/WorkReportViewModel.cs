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
        public IList<LeaveViewModel> Leanves { get; set; }
        public IList<MeetingViewModel> Meetings { get; set; }
        public IList<MissionViewModel> Missions { get; set; }
        public IList<PreparationDocumentViewModel> PreparationDocuments { get; set; }
    }
}
