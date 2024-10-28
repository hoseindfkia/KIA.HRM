using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;
using Share;
using ViewModel.WorkReport.PreparationDocument;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers.WorkReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreparationDocumentController : ControllerBase
    {
        private readonly IPreparationDocumentService _preparationDocumentService;
        private readonly IMissionService _missionService;
        private readonly IMeetingService _meetingService;
        private readonly ILeaveService _leaveService;

        public PreparationDocumentController(IPreparationDocumentService preparationDocumentService,
                                             IMissionService missionService,
                                             IMeetingService meetingService,
                                             ILeaveService leaveService)
        {
            _preparationDocumentService = preparationDocumentService;  
            _missionService = missionService;
            _meetingService = meetingService;
            _leaveService = leaveService;
        }

        
        [HttpPost("AddPreparationDocument")]
        public async Task<Feedback<int>> Post(PreparationDocumentPostViewModel PreparationDocumentPost )
        {
            var outMessage = "";
            var leave = await _leaveService.OverlapCheck(PreparationDocumentPost.FromDate, PreparationDocumentPost.ToDate);
            if (leave.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + leave.ExceptionMessage;
            var mission = await _missionService.OverlapCheck(PreparationDocumentPost.FromDate, PreparationDocumentPost.ToDate);
            if (mission.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + mission.ExceptionMessage;
            var meeting = await _meetingService.OverlapCheck(PreparationDocumentPost.FromDate, PreparationDocumentPost.ToDate);
            if (meeting.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + meeting.ExceptionMessage;
            var preparationDocument = await _preparationDocumentService.OverlapCheck(PreparationDocumentPost.FromDate, PreparationDocumentPost.ToDate);
            if (preparationDocument.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + preparationDocument.ExceptionMessage;
            var leaveDuplicate = await _preparationDocumentService.DuplicateCheck(PreparationDocumentPost.FromDate, PreparationDocumentPost.ToDate);
            if (leaveDuplicate.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + leaveDuplicate.ExceptionMessage;

            if (outMessage != "")
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Error, 0, outMessage);


            if (!ModelState.IsValid)
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.InvalidDataFormat, Share.Enum.MessageType.Error, 0, ModelState.GetModelStateErrors());
            return await _preparationDocumentService.AddAsycn(PreparationDocumentPost, UserId: 0);
        }

    }
}
