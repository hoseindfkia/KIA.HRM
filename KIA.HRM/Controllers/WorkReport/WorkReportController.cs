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
        public async Task<Feedback<WorkReportViewModel>> GetByDate(DateTime dateTime)//)string dateTime
        {
            var UserId = 0;//user.identity
            var FbOut = new Feedback<WorkReportViewModel>();
            var leaves = await _leaveService.GetByDateAsync(dateTime, UserId);
            var Meetings = await _meetingService.GetByDateAsync(dateTime, UserId);
            var Missions = await _missionService.GetByDate(dateTime, UserId);
            var PreparationDocuments = await _preparationDocumentService.GetByDateAsync(dateTime, UserId);

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
                Missions = Missions.Value,
                PreparationDocuments = PreparationDocuments.Value,
                StartWorkTime = startTime,
                EndWorkTime = new TimeOnly(17, 0, 0),
                AllTimeWorked = (long)timeDifference.TotalMinutes,
                SelectedDayPersian = dateTime.ToPersianDate(),
                MorningDurationMinuets = (long)morningMinutes.TotalMinutes,
                NightDurationMinuets = (long)nightMinutes.TotalMinutes + 1,
                TimeSlot = getTimes(leaves.Value, Meetings.Value, Missions.Value, PreparationDocuments.Value, startTime, endTime)
            };
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, WorkReport, "");
        }



        //TODO: این تابع باید در شیر قرار گیرد
        private List<TimeSlotViewModel> getTimes(IList<LeaveViewModel> leave,
                                             IList<MeetingViewModel> meeting,
                                             IList<MissionViewModel> mission,
                                             IList<PreparationDocumentViewModel> preparationDocument,
                                             TimeOnly startTime,
                                              TimeOnly endTime)
        {
            var isAnyWorkReport = false;
            var TimeSlotListFirst = new List<TimeSlotViewModel>();
            var TimeSlotListSorted = new List<TimeSlotViewModel>();

            if (leave != null && leave.Any())
            {
                foreach (var itemLeave in leave)
                {
                    // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                    var isOneDay = CheckEqualDate((DateTime)itemLeave.FromDatePersian.ToEnglishDateTime(), (DateTime)itemLeave.ToDatePersian.ToEnglishDateTime());
                    var TimeSlot = new TimeSlotViewModel()
                    {
                        Id = itemLeave.Id,
                        TimeStart = getTimeSpanOfDatePersian(itemLeave.FromDatePersian),
                        TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(itemLeave.ToDatePersian),
                        Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.leave),
                        WorkReportType = Share.Enum.WorkReportType.leave,
                        DurationMinuets = itemLeave.DurationMinuets,
                    };
                    TimeSlotListFirst.Add(TimeSlot);
                    isAnyWorkReport = true;
                }

                //var leaveModel = leave.FirstOrDefault();
                //// آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                //var isOneDay = CheckEqualDate((DateTime)leaveModel.FromDatePersian.ToEnglishDateTime() , (DateTime)leaveModel.ToDatePersian.ToEnglishDateTime());
                //var TimeSlot = new TimeSlotViewModel()
                //{
                //    TimeStart = getTimeSpanOfDatePersian(leave.FirstOrDefault().FromDatePersian),
                //    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(leave.FirstOrDefault().ToDatePersian),
                //    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.leave),
                //    WorkReportType = Share.Enum.WorkReportType.leave,
                //    DurationMinuets = leaveModel.DurationMinuets,
                //};
                //TimeSlotListFirst.Add(TimeSlot);
                //isAnyWorkReport = true;
            }
            if (meeting != null && meeting.Any())
            {
                foreach (var itemMeeting in meeting)
                {
                    // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                    var isOneDay = CheckEqualDate((DateTime)itemMeeting.FromDatePersian.ToEnglishDateTime(), (DateTime)itemMeeting.ToDatePersian.ToEnglishDateTime());
                    var TimeSlot = new TimeSlotViewModel()
                    {
                        Id = itemMeeting.Id,
                        TimeStart = getTimeSpanOfDatePersian(itemMeeting.FromDatePersian),
                        TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(itemMeeting.ToDatePersian),
                        Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.meeting),
                        WorkReportType = Share.Enum.WorkReportType.meeting,
                        DurationMinuets = itemMeeting.DurationMinuets
                    };
                    TimeSlotListFirst.Add(TimeSlot);
                    isAnyWorkReport = true;
                }
                //var meetingModel = meeting.FirstOrDefault();    
                //// آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                //var isOneDay = CheckEqualDate((DateTime)meetingModel.FromDatePersian.ToEnglishDateTime() , (DateTime)meetingModel.ToDatePersian.ToEnglishDateTime());
                //var TimeSlot = new TimeSlotViewModel()
                //{
                //    TimeStart = getTimeSpanOfDatePersian(meeting.FirstOrDefault().FromDatePersian),
                //    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(meeting.FirstOrDefault().ToDatePersian),
                //    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.meeting),
                //    WorkReportType = Share.Enum.WorkReportType.meeting,
                //    DurationMinuets = meetingModel.DurationMinuets
                //};
                //TimeSlotListFirst.Add(TimeSlot);
                //isAnyWorkReport = true;
            }
            if (mission != null && mission.Any())
            {
                foreach (var itemMission in mission)
                {
                    // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                    var isOneDay = CheckEqualDate((DateTime)itemMission.FromDatePersian.ToEnglishDateTime(), (DateTime)itemMission.ToDatePersian.ToEnglishDateTime());
                    var TimeSlot = new TimeSlotViewModel()
                    {
                        Id = itemMission.Id,
                        TimeStart = getTimeSpanOfDatePersian(itemMission.FromDatePersian),
                        TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(itemMission.ToDatePersian),
                        Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.mission),
                        WorkReportType = Share.Enum.WorkReportType.mission,
                        DurationMinuets = itemMission.DurationMinuets
                    };
                    TimeSlotListFirst.Add(TimeSlot);
                    isAnyWorkReport = true;
                }
                //var missionModel = mission.FirstOrDefault();
                //// آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                //var isOneDay = CheckEqualDate((DateTime)missionModel.FromDatePersian.ToEnglishDateTime() , (DateTime)missionModel.ToDatePersian.ToEnglishDateTime());
                //var TimeSlot = new TimeSlotViewModel()
                //{
                //    Id = missionModel.Id,
                //    TimeStart = getTimeSpanOfDatePersian(mission.FirstOrDefault().FromDatePersian),
                //    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(mission.FirstOrDefault().ToDatePersian),
                //    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.mission),
                //    WorkReportType = Share.Enum.WorkReportType.mission,
                //    DurationMinuets=missionModel.DurationMinuets
                //};
                //TimeSlotListFirst.Add(TimeSlot);
                //isAnyWorkReport = true;
            }
            if (preparationDocument != null && preparationDocument.Any())
            {
                foreach (var itemPreparation in preparationDocument)
                {
                    // آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                    var isOneDay = CheckEqualDate((DateTime)itemPreparation.FromDatePersian.ToEnglishDateTime(), (DateTime)itemPreparation.ToDatePersian.ToEnglishDateTime());
                    var TimeSlot = new TimeSlotViewModel()
                    {
                        Id = itemPreparation.Id,
                        TimeStart = getTimeSpanOfDatePersian(itemPreparation.FromDatePersian),
                        TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(itemPreparation.ToDatePersian),
                        Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.preparationDocument),
                        WorkReportType = Share.Enum.WorkReportType.preparationDocument,
                        DurationMinuets = itemPreparation.DurationMinuets
                    };
                    TimeSlotListFirst.Add(TimeSlot);
                    isAnyWorkReport = true;
                }
                //var preparationModel = preparationDocument.FirstOrDefault();
                //// آیا عملیات در هامان روز است یا انتهای آن به روز های دیگر نیز می انجامد
                //var isOneDay = CheckEqualDate((DateTime)preparationModel.FromDatePersian.ToEnglishDateTime(), (DateTime)preparationModel.ToDatePersian.ToEnglishDateTime());
                //var TimeSlot = new TimeSlotViewModel()
                //{
                //    TimeStart = getTimeSpanOfDatePersian(preparationDocument.FirstOrDefault().FromDatePersian),
                //    TimeEnd = !isOneDay ? endTime : getTimeSpanOfDatePersian(preparationDocument.FirstOrDefault().ToDatePersian),
                //    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.preparationDocument),
                //    WorkReportType = Share.Enum.WorkReportType.preparationDocument,
                //    DurationMinuets = preparationModel.DurationMinuets
                //};
                //TimeSlotListFirst.Add(TimeSlot);
                //isAnyWorkReport = true;
            }

            TimeSlotListFirst = TimeSlotListFirst.OrderBy(x => x.TimeStart).ToList();

            TimeOnly time = new TimeOnly(0, 0);
            TimeSlotListSorted.Add(new TimeSlotViewModel()
            {
                TimeStart = time,
                TimeEnd = startTime,
                Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.morning),
                WorkReportType = Share.Enum.WorkReportType.morning,
                DurationMinuets = (long)(startTime - time).TotalMinutes
            });
            time = startTime;
            if (TimeSlotListFirst.Any())
            {

                foreach (var timeSlot in TimeSlotListFirst)
                {

                    if (timeSlot.TimeStart > time)
                    {
                        TimeSlotListSorted.Add(new TimeSlotViewModel()
                        {
                            Id = timeSlot.Id,
                            TimeStart = time,
                            TimeEnd = timeSlot.TimeStart,
                            Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.nothing),
                            WorkReportType = Share.Enum.WorkReportType.nothing,
                            DurationMinuets = (long)(timeSlot.TimeStart - time).TotalMinutes

                        });
                        time = timeSlot.TimeStart;
                    }
                    TimeSlotListSorted.Add(new TimeSlotViewModel()
                    {
                        Id = timeSlot.Id,
                        TimeStart = timeSlot.TimeStart,
                        TimeEnd = timeSlot.TimeEnd,
                        Title = timeSlot.Title,
                        WorkReportType = timeSlot.WorkReportType,
                        DurationMinuets = timeSlot.DurationMinuets,
                    });
                    time = timeSlot.TimeEnd;
                }
            }
            else
            {
                TimeSlotListSorted.Add(new TimeSlotViewModel()
                {
                    TimeStart = startTime,
                    TimeEnd = endTime,
                    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.nothing),
                    WorkReportType = Share.Enum.WorkReportType.nothing,
                    DurationMinuets = (long)(endTime - startTime).TotalMinutes

                });
            }


            // اگر قبل از آخر وقت هم کاری نبود که انجام داده باشد بای هیچ زده شود 
            if (time < endTime && isAnyWorkReport)
            {
                TimeSlotListSorted.Add(new TimeSlotViewModel()
                {
                    TimeStart = time,
                    TimeEnd = endTime,
                    Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.nothing),
                    WorkReportType = Share.Enum.WorkReportType.nothing,
                    DurationMinuets = (long)(endTime - time).TotalMinutes
                });
            }

            TimeSlotListSorted.Add(new TimeSlotViewModel()
            {
                TimeStart = endTime,
                TimeEnd = new TimeOnly(23, 59),
                Title = Utility.GetDescriptionOfEnum(typeof(Share.Enum.WorkReportType), Share.Enum.WorkReportType.night),
                WorkReportType = Share.Enum.WorkReportType.night,
                DurationMinuets = (long)(new TimeOnly(23, 59) - endTime).TotalMinutes

            });
            return TimeSlotListSorted;
        }

        private TimeOnly getTimeSpanOfDatePersian(string datePersian)
        {
            var dateTime = datePersian.ToEnglishDateTime();
            if (dateTime == null)
                return new TimeOnly(-1);
            var OutTimeSpan = new TimeOnly(((DateTime)dateTime).Hour, ((DateTime)dateTime).Minute, 0);
            return OutTimeSpan;
        }

        private bool CheckEqualDate(DateTime date1, DateTime date2)
        {
            return date1.Date.Equals(date2.Date);
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
