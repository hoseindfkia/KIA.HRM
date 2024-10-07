using KIA.HRM.Authorization.ClaimBasedAuthorization.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Project;
using Share;
using ViewModel.Project;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GETAll
       [CustomAuthorization("ProjectList")]
        //[Authorize]
        [HttpGet("GetAll")]

        public async Task<Feedback<IList<ProjectGetAllViewModel>>> GetAllAsync()
        {
            return await _projectService.GetAllAsync(); 
        }

        /// <summary>
        ///  جهت دراپ داون
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDropDown")]
        public async Task<Feedback<IList<ProjectGetAllDropDownViewModel>>> GetAllDropDownAsync()
        {
            return await _projectService.GetAllDropDownAsync(); 
        }

    }
}
