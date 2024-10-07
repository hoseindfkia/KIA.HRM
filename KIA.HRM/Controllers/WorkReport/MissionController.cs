using Microsoft.AspNetCore.Mvc;
using Service.WorkReport.Mission;
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
        public MissionController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        // POST api/<MissionController>
        [HttpPost]
        public async Task<Feedback<int>> Post(MissionPostViewModel MissionPost)
        {
            return await _missionService.AddAsycn(MissionPost, UserId: 0);
        }

       
    }
}
