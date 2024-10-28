﻿using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;
using Share;
using ViewModel.WorkReport.Leave;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers.WorkReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        private readonly IMeetingService _meetingService;
        private readonly IMissionService _missionService;
        private readonly IPreparationDocumentService _preparationDocumentService;

        public LeaveController(ILeaveService leaveService,
                               IMeetingService meetingService,
                               IMissionService missionService,
                               IPreparationDocumentService preparationDocumentService)
        {
            _leaveService = leaveService;
            _meetingService = meetingService;
            _missionService = missionService;
            _preparationDocumentService = preparationDocumentService;
        }


        [HttpPost("AddLeave")]
        public async Task<Feedback<int>> Post(LeavePostViewModel LeavePost)
        {
            var outMessage = "";
            var leave = await _leaveService.OverlapCheck(LeavePost.FromDate, LeavePost.ToDate);
            if (leave.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + leave.ExceptionMessage;
            var mission = await _missionService.OverlapCheck(LeavePost.FromDate, LeavePost.ToDate);
            if (mission.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + mission.ExceptionMessage;
            var meeting = await _meetingService.OverlapCheck(LeavePost.FromDate, LeavePost.ToDate);
            if (meeting.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + meeting.ExceptionMessage;
            var preparationDocument = await _preparationDocumentService.OverlapCheck(LeavePost.FromDate, LeavePost.ToDate);
            if (preparationDocument.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" + preparationDocument.ExceptionMessage;
            var leaveDuplicate = await _leaveService.DuplicateCheck(LeavePost.FromDate, LeavePost.ToDate);
            if (leaveDuplicate.Status == Share.Enum.FeedbackStatus.DataIsIsAvailable)
                outMessage += "-" +leaveDuplicate.ExceptionMessage;

            if (outMessage != "")
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Error, 0, outMessage);


            if (!ModelState.IsValid)
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.InvalidDataFormat, Share.Enum.MessageType.Error, 0, ModelState.GetModelStateErrors());
            var UserId = 0;
            return await _leaveService.AddAsycn(LeavePost, UserId);
        }

    }
}
