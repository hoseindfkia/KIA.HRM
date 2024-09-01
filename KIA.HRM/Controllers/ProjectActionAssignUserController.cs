using Microsoft.AspNetCore.Mvc;
using Service.ProjectActionAssignUser;
using Share;
using Share.Enum;
using ViewModel.ProjectActionAssignUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectActionAssignUserController : ControllerBase
    {
        private readonly IProjectActionAssignUserService _projectActionAssignUserService;
        public ProjectActionAssignUserController(IProjectActionAssignUserService projectActionAssignUserService)
        {
            _projectActionAssignUserService = projectActionAssignUserService;   
        }


        /// <summary>
        /// پروژه را دریافت و به بخش دیگر ارسال می گردد
        /// </summary>
        /// <param name="projectActionAssignUserPostViewModel"></param>
        /// <returns></returns>
        [HttpPost("AddProjectActionAssignUser")]
        public async Task<Feedback<int>> AddProjectActionAssignUserAsycn([FromBody] ProjectActionAssignUserPostViewModel projectActionAssignUserPostViewModel)
        {
            return await _projectActionAssignUserService.AddProjectActionAssignUserAsycn(projectActionAssignUserPostViewModel,ProjectActionStatusType.OpenToAssign);
        }

    }
}
