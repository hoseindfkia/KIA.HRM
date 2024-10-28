using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;
using Share;
using ViewModel.WorkReport.Meeting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers.WorkReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ILeaveService _leaveService;
        private readonly IMissionService _missionService;
        private readonly IPreparationDocumentService _preparationDocumentService;

        public MeetingController(IMeetingService meetingService,
                                 ILeaveService leaveService,
                                 IMissionService missionService,
                                 IPreparationDocumentService preparationDocumentService)
        {
            _meetingService = meetingService;
            _leaveService = leaveService;
            _missionService = missionService;
            _preparationDocumentService = preparationDocumentService;
        }

        // POST api/<MeetingController>
        [HttpPost("AddMeeting")]
        public async Task<Feedback<int>> Post([FromBody]MeetingPostViewModel MeetingPost)
        {
            var outMessage = "";
            var leave = await _leaveService.OverlapCheck(MeetingPost.FromDate, MeetingPost.ToDate);
            if (leave.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + leave.ExceptionMessage;
            var mission = await _missionService.OverlapCheck(MeetingPost.FromDate, MeetingPost.ToDate);
            if (mission.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + mission.ExceptionMessage;
            var meeting = await _meetingService.OverlapCheck(MeetingPost.FromDate, MeetingPost.ToDate);
            if (meeting.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + meeting.ExceptionMessage;
            var preparationDocument = await _preparationDocumentService.OverlapCheck(MeetingPost.FromDate, MeetingPost.ToDate);
            if (preparationDocument.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + preparationDocument.ExceptionMessage;

            var leaveDuplicate = await _meetingService.DuplicateCheck(MeetingPost.FromDate, MeetingPost.ToDate);
            if (leaveDuplicate.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + leaveDuplicate.ExceptionMessage;

            if (outMessage != "")
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Error, 0, outMessage);



            if (!ModelState.IsValid)
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.InvalidDataFormat, Share.Enum.MessageType.Error, 0, ModelState.GetModelStateErrors());
            return await _meetingService.AddAsycn(MeetingPost, UserId: 0);
        }

      
    }
}
