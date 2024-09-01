using DomainClass.Main;
using FileService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Service.ProjectAction;
using Service.ProjectActionAssignUser;
using Share;
using Share.Enum;
using ViewModel.ProjectAction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectActionController : ControllerBase
    {
        private readonly IProjectActionService _projectActionService;
        private readonly IProjectActionAssignUserService _projectActionAssignUserService;
        private readonly IFileManagerService _fileManagerService;
        private readonly AppSettings _mySettings;
        public ProjectActionController(IProjectActionService projectActionService,
                                       IFileManagerService fileManagerService,
                                       IProjectActionAssignUserService projectActionAssignUserService,
                                       IOptions<AppSettings> mySettings)
        {
            _projectActionService = projectActionService;
            _fileManagerService = fileManagerService;
            _projectActionAssignUserService = projectActionAssignUserService;
            _mySettings = mySettings.Value;
        }

        /// <summary>
        /// دریافت تمامی پروژه ها
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProjectCartable/{projectActionStatus}")]
        public async Task<Feedback<IList<ProjectActionCartableGetAllViewModel>>> GetAllProjectActionAsyn(ProjectActionStatusType projectActionStatus)
        {
            return await _projectActionService.GetAllProjectCartableAsync(projectActionStatus);
        }

        /// <summary>
        /// ثبت یک عملیات پروژه
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("AddProjectAction")]
        public async Task<Feedback<int>> AddProjectActionAsycn([FromBody] ProjectActionPostViewModel ProjectActionPostViewModel)
        {
            var FbOut = new Feedback<int>();
            if (ModelState.IsValid)
            {
                foreach (var itemFile in ProjectActionPostViewModel.Files)
                {
                    var IsEncryptionFiles = _mySettings.IsEncryptionFiles;
                    var MoveFile = _fileManagerService.Move(FormType.Project, itemFile.Url, FileType.Image);
                    if (MoveFile.Status == FeedbackStatus.UpdatedSuccessful)
                        itemFile.Url = MoveFile.Value;
                }
                return await _projectActionService.AddProjectActionAsycn(ProjectActionPostViewModel);
            }
            else
            {
                FbOut.SetFeedback(FeedbackStatus.InvalidDataFormat, MessageType.Error, ModelState);
                return FbOut;
            }
        }

        /// <summary>
        /// دریافت تمامی پروژه ها
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProjectCartable/{Id}")]
        public async Task<Feedback<ProjectActionCartableViewModel>> GetProjectCartableAsync(long Id)
        {
            return await _projectActionService.GetProjectCartableAsync(Id);
        }

        /// <summary>
        /// ثبت یک عملیات پروژه
        /// کاربر اعلام می کند پروژه را دریافت کرده و بر روی آن کار انجام می دهد. 
        /// در این حالت پروژه برای اشخاص دیگر نمایش داده نمی شود
        /// </summary>
        /// <param name="value"></param>
        [HttpGet("ChangeProjectActionStatusToInAction/{Id}")]
        public async Task<Feedback<int>> ChangeProjectActionStatusAsync(long Id)
        {
            var FbOut = new Feedback<int>();
            FbOut.SetFeedback(FeedbackStatus.FileIsNotFound, MessageType.Error, 0, "");
            var AddProjectActionAssignUser = await _projectActionAssignUserService.AddProjectActionAssignUserAsycn(new ViewModel.ProjectActionAssignUser.ProjectActionAssignUserPostViewModel()
            {
                Comment = "",
                ProjectActionId = Id,
            }, ProjectActionStatusType.InAction);
            if (AddProjectActionAssignUser.Status == FeedbackStatus.UpdatedSuccessful)
            {
                return await _projectActionService.ChangeProjectActionStatusAsync(Id, ProjectActionStatusType.InAction, true);
            }
            else
                return FbOut;
        }

        /// <summary>
        /// ثبت یک عملیات پروژه
        /// کاربر اعلام می کند پروژه را دریافت کرده و بر روی آن کار انجام می دهد. 
        /// در این حالت پروژه برای اشخاص دیگر نمایش داده نمی شود
        /// </summary>
        /// <param name="value"></param>
        [HttpGet("ChangeProjectActionStatusToArchive/{Id}")]
        public async Task<Feedback<int>> ChangeProjectActionStatusToArchiveAsync(long Id)
        {
            var FbOut = new Feedback<int>();
            FbOut.SetFeedback(FeedbackStatus.FileIsNotFound, MessageType.Error, 0, "");
            var AddProjectActionAssignUser = await _projectActionAssignUserService.AddProjectActionAssignUserAsycn(new ViewModel.ProjectActionAssignUser.ProjectActionAssignUserPostViewModel()
            {
                Comment = "",
                ProjectActionId = Id,
            }, ProjectActionStatusType.Archive);
            if (AddProjectActionAssignUser.Status == FeedbackStatus.UpdatedSuccessful)
            {
                return await _projectActionService.ChangeProjectActionStatusAsync(Id, ProjectActionStatusType.Archive, true);
            }
            else
                return FbOut;
        }
    }
}
