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


        [HttpPost]
        public Task<Feedback<int>> Post(LeavePostViewModel LeavePost)
        {
            var UserId = 0;
            return _leaveService.AddAsycn(LeavePost, UserId);
        }

    }
}
