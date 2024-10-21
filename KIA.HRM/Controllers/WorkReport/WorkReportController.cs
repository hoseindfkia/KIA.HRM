using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;
using Share;
using ViewModel.WorkReport.Leave;
using ViewModel.WorkReport.Meeting;
using ViewModel.WorkReport.Mission;
using ViewModel.WorkReport.PreparationDocument;
using ViewModel.WorkReport.WorkReport;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers.WorkReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkReportController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        private readonly IMeetingService _meetingService;
        private readonly IMissionService _missionService;
        private readonly IPreparationDocumentService _preparationDocumentService;
        public WorkReportController(ILeaveService leaveService,
                                    IMeetingService meetingService,
                                    IMissionService missionService,
                                    IPreparationDocumentService preparationDocumentService)
        {
            _leaveService = leaveService;
            _meetingService = meetingService;
            _missionService = missionService;
            _preparationDocumentService = preparationDocumentService;
        }


        /// <summary>
        /// دریافت مقادیر گزارش کار در روز مشخص ارسالی
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet("GetByDate")]
        public async Task<Feedback<WorkReportViewModel>> GetByDate(DateTime dateTime)
        {
            var UserId = 0;//user.identity
            var FbOut = new Feedback<WorkReportViewModel>();
            var leaves = await _leaveService.GetByDateAsync(dateTime, UserId);
            var Meetings = await _meetingService.GetByDateAsync(dateTime, UserId);
            var Mission = await _missionService.GetByDate(dateTime, UserId);
            var PreparationDocument = await _preparationDocumentService.GetByDateAsync(dateTime, UserId);

            //TODO:  ساعت باید از ساعت زن خوانده شود
            TimeOnly startTime = new TimeOnly(7, 30, 0); // 07:30 AM  
            TimeOnly endTime = new TimeOnly(17, 0, 0); // 05:00 PM  
            TimeSpan timeDifference = endTime - startTime;
            TimeSpan morningMinutes = startTime - new TimeOnly(00, 00);
            TimeSpan nightMinutes = new TimeOnly(23, 59) - endTime;


            var WorkReport = new WorkReportViewModel()
            {
                Leaves = leaves.Value,
                Meetings = Meetings.Value,
                Missions = Mission.Value,
                PreparationDocuments = PreparationDocument.Value,
                StartWorkTime = startTime,
                EndWorkTime = new TimeOnly(17, 0, 0),
                AllTimeWorked = (long)timeDifference.TotalMinutes,
                SelectedDayPersian = dateTime.ToPersianDate(),
                MorningDurationMinuets = (long)morningMinutes.TotalMinutes,
                NightDurationMinuets = (long)nightMinutes.TotalMinutes + 1
            };
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, WorkReport, "");
        }



        //TODO: این تابع باید درست شود
        private WorkReportViewModel getTimes(List<LeaveViewModel> leave,
                                             List<MeetingViewModel> meeting,
                                             List<MissionViewModel> mission,
                                             List<PreparationDocumentViewModel> preparationDocument,
                                             TimeOnly startTime,
                                              TimeOnly endTime)
        {

            var TimeSlotListFirst = new List<TimeSlotViewModel>();
            if (leave.Any())
            {
                // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                var  isOneDay = leave.FirstOrDefault().FromDatePersian.ToEnglishDateTime() == leave.FirstOrDefault().ToDatePersian.ToEnglishDateTime();
                var TimeSlot = new TimeSlotViewModel()
                {
                    TimeStart = getTimeSpanOfDatePersian(leave.FirstOrDefault().FromDatePersian),
                    TimeEnd  = !isOneDay  ? endTime : getTimeSpanOfDatePersian(leave.FirstOrDefault().ToDatePersian),
                    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.leave),
                    WorkReportType = Share.Enum.WorkReportType.leave,
                };
                TimeSlotListFirst.Add(TimeSlot);
            }
            if (meeting.Any())
            {
                // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                var isOneDay = meeting.FirstOrDefault().FromDatePersian.ToEnglishDateTime() == meeting.FirstOrDefault().ToDatePersian.ToEnglishDateTime();
                var TimeSlot = new TimeSlotViewModel()
                {
                    TimeStart = getTimeSpanOfDatePersian(meeting.FirstOrDefault().FromDatePersian),
                    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(meeting.FirstOrDefault().ToDatePersian),
                    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.meeting),
                    WorkReportType = Share.Enum.WorkReportType.meeting,
                };
                TimeSlotListFirst.Add(TimeSlot);
            }
            if (mission.Any())
            {
                // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                var isOneDay = mission.FirstOrDefault().FromDatePersian.ToEnglishDateTime() == mission.FirstOrDefault().ToDatePersian.ToEnglishDateTime();
                var TimeSlot = new TimeSlotViewModel()
                {
                    TimeStart = getTimeSpanOfDatePersian(mission.FirstOrDefault().FromDatePersian),
                    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(mission.FirstOrDefault().ToDatePersian),
                    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.mission),
                    WorkReportType = Share.Enum.WorkReportType.mission,
                };
                TimeSlotListFirst.Add(TimeSlot);
            }
            if (preparationDocument.Any())
            {
                // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                var isOneDay = preparationDocument.FirstOrDefault().FromDatePersian.ToEnglishDateTime() == preparationDocument.FirstOrDefault().ToDatePersian.ToEnglishDateTime();
                var TimeSlot = new TimeSlotViewModel()
                {
                    TimeStart = getTimeSpanOfDatePersian(preparationDocument.FirstOrDefault().FromDatePersian),
                    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(preparationDocument.FirstOrDefault().ToDatePersian),
                    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.preparationDocument),
                    WorkReportType = Share.Enum.WorkReportType.preparationDocument,
                };
                TimeSlotListFirst.Add(TimeSlot);
            }


            TimeOnly time = new TimeOnly(0, 0);


            while (time < new TimeOnly(23, 59))
            {
                var timeSlot = new TimeSlotViewModel();





            }




            return new WorkReportViewModel();
        }

        private TimeOnly getTimeSpanOfDatePersian(string datePersian)
        {
            var dateTime = datePersian.ToEnglishDateTime();
            if (dateTime == null)
                return new TimeOnly(-1);
            var OutTimeSpan = new TimeOnly(((DateTime)dateTime).Hour, ((DateTime)dateTime).Minute,0);
            return OutTimeSpan;
        }



        //public static List<Dictionary<string, object>> GenerateWorkSchedule(List<WorkActivity> activities)
        //{
        //    var timeSlots = new List<TimeSlotViewModel>
        //        {
        //        new TimeSlotViewModel { Title = "صبح", TimeStart = TimeSpan.Parse("00:00"), TimeEnd = TimeSpan.Parse("07:30") },
        //        new TimeSlotViewModel { Title = "جلسه", TimeStart = TimeSpan.Parse("07:30"), TimeEnd = TimeSpan.Parse("09:30") },
        //        new TimeSlotViewModel { Title = "هیچ", TimeStart = TimeSpan.Parse("09:30"), TimeEnd = TimeSpan.Parse("10:30") },
        //        new TimeSlotViewModel { Title = "ماموریت", TimeStart = TimeSpan.Parse("10:30"), TimeEnd = TimeSpan.Parse("14:30") },
        //        new TimeSlotViewModel { Title = "مرخصی", TimeStart = TimeSpan.Parse("14:30"), TimeEnd = TimeSpan.Parse("16:30") },
        //        new TimeSlotViewModel { Title = "هیچ", TimeStart = TimeSpan.Parse("16:30"), TimeEnd = TimeSpan.Parse("17:30") },
        //        new TimeSlotViewModel { Title = "شب", TimeStart = TimeSpan.Parse("17:30"), TimeEnd = TimeSpan.Parse("23:59") },
        //        };

        //    var date = new DateTime(2024, 10, 21);
        //    var workSchedule = new List<Dictionary<string, object>>();

        //    foreach (var slot in timeSlots)
        //    {
        //        var activity = activities.FirstOrDefault(a => a.Title == slot.Title);

        //        if (activity != null)
        //        {
        //            workSchedule.Add(new Dictionary<string, object>
        //                {
        //                { "title", activity.Title },
        //                { "workType", activity.WorkType },
        //                { "fromDate", $"{date:yyyy/MM/dd} {slot.TimeStart:hh\\:mm}" },
        //                { "toDate", $"{date:yyyy/MM/dd} {slot.TimeEnd:hh\\:mm}" }
        //                });
        //        }
        //        else
        //        {
        //            workSchedule.Add(new Dictionary<string, object>
        //                    {
        //                    { "title", "هیچ" },
        //                    { "workType",0 },
        //                    { "fromDate", $"{date:yyyy/MM/dd} {slot.TimeStart:hh\\:mm}" },
        //                    { "toDate", $"{date:yyyy/MM/dd} {slot.TimeEnd:hh\\:mm}" }
        //                    });
        //        }
        //    }

        //    return workSchedule;
        //}

    }
}
