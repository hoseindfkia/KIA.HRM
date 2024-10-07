using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;
using Share;
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
        [HttpGet]
        public async Task<Feedback<WorkReportViewModel>> Get(DateTime dateTime)
        {
            var FbOut = new Feedback<WorkReportViewModel>();
            var leaves = await _leaveService.GetByDateAsync(dateTime, UserId: 0);
            var Meetings = await _meetingService.GetByDateAsync(dateTime, UserId: 0);
            var Mission = await _missionService.GetByDate(dateTime, UserId: 0);
            var PreparationDocument = await _preparationDocumentService.GetByDateAsync(dateTime, UserId: 0);

            var WorkReport = new WorkReportViewModel()
            {
                Leanves = leaves.Value,
                Meetings = Meetings.Value,
                Missions = Mission.Value,
                PreparationDocuments = PreparationDocument.Value,
            };
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, WorkReport, "");
        }

    }
}
