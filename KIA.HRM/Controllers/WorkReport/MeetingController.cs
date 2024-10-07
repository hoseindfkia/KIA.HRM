using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Meeting;
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
        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        // POST api/<MeetingController>
        [HttpPost]
        public async Task<Feedback<int>> Post(MeetingPostViewModel MeetingPost)
        {
            return await _meetingService.AddAsycn(MeetingPost, UserId: 0);
        }

      
    }
}
