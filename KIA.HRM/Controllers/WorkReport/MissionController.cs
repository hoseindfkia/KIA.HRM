using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;
using Share;
using ViewModel.WorkReport.Mission;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers.WorkReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IMeetingService _meetingService;
        private readonly ILeaveService _leaveService;
        private readonly IPreparationDocumentService _preparationDocumentService;

        public MissionController(IMissionService missionService,
                                 IMeetingService meetingService,
                                 ILeaveService leaveService,
                                 IPreparationDocumentService preparationDocumentService)
        {
            _missionService = missionService;
            _meetingService = meetingService;
            _leaveService = leaveService;
            _preparationDocumentService = preparationDocumentService;
        }

        // POST api/<MissionController>
        [HttpPost("AddMission")]
        public async Task<Feedback<int>> Post(MissionPostViewModel MissionPost)
        {
            var outMessage = "";
            var leave = await _leaveService.OverlapCheck(MissionPost.FromDate, MissionPost.ToDate);
            if (leave.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + leave.ExceptionMessage;
            var mission = await _missionService.OverlapCheck(MissionPost.FromDate, MissionPost.ToDate);
            if (mission.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + mission.ExceptionMessage;
            var meeting = await _meetingService.OverlapCheck(MissionPost.FromDate, MissionPost.ToDate);
            if (meeting.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + meeting.ExceptionMessage;
            var preparationDocument = await _preparationDocumentService.OverlapCheck(MissionPost.FromDate, MissionPost.ToDate);
            if (preparationDocument.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + preparationDocument.ExceptionMessage;
            //var leaveDuplicate = await _leaveService.DuplicateCheck(MissionPost.FromDate, MissionPost.ToDate);
            //if (leaveDuplicate.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
            //    outMessage += "-" + leaveDuplicate.ExceptionMessage;

            if (outMessage != "")
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Error, 0, outMessage);


            if (!ModelState.IsValid)
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.InvalidDataFormat, Share.Enum.MessageType.Error, 0, ModelState.GetModelStateErrors());
            return await _missionService.AddAsycn(MissionPost, UserId: 0);
        }


    }
}
