using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Leave;
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
        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }


        [HttpPost("AddLeave")]
        public async Task<Feedback<int>> Post(LeavePostViewModel LeavePost)
        {
            if (!ModelState.IsValid)
                return (new Feedback<int>()).SetFeedbackNew(Share.Enum.FeedbackStatus.InvalidDataFormat, Share.Enum.MessageType.Error, 0, ModelState.GetModelStateErrors());
            var UserId = 0;
            return await _leaveService.AddAsycn(LeavePost, UserId);
        }

    }
}
