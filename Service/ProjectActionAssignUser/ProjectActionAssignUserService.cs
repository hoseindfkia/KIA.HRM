using DataLayer;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Service.ProjectAction;
using Share;
using Share.Enum;
using ViewModel.ProjectActionAssignUser;

namespace Service.ProjectActionAssignUser
{
    public class ProjectActionAssignUserService : IProjectActionAssignUserService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<ProjectActionAssignUserEntity> _Entity;
        private readonly IProjectActionService _projectActionService;

        public ProjectActionAssignUserService(IUnitOfWorkContext context, IProjectActionService projectActionService)
        {
            _Context = context;
            _Entity = _Context.Set<ProjectActionAssignUserEntity>();
            _projectActionService = projectActionService;
        }

        /// <summary>
        /// یک پروژه گرفته می شود و برای ارسال به بخش های دیگر کامنت گذاری می شود
        /// </summary>
        /// <param name="projectActionAssignUserPostViewModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Feedback<int>> AddProjectActionAssignUserAsycn(ProjectActionAssignUserPostViewModel projectActionAssignUserPostViewModel, ProjectActionStatusType statusType)
        {
            var FbOut = new Feedback<int>();
            try
            {
                var ProjectAction = await _projectActionService.ChangeProjectActionStatusAsync(projectActionAssignUserPostViewModel.ProjectActionId, ProjectActionStatusType.OpenToAssign, true);
                if (ProjectAction.Status == FeedbackStatus.UpdatedSuccessful)
                {
                    var Model = new ProjectActionAssignUserEntity()
                    {
                        Comment = projectActionAssignUserPostViewModel.Comment,
                        ProjectActionId = projectActionAssignUserPostViewModel.ProjectActionId,
                        UserAssignedId = 5,  // خود کاربری که دارد این مورد را ثبت می کند یعنی به نام خود ثبت کرده است
                        UserRoleId = 2, // نقش کاربر است که  مشخص می شود در نقش فعلی این پروژ] را قبول کرده است
                        ProjectActionStatusType = statusType,  /// در این حالت وضعیت پروژه اکشن نیز باید به حالت ارسال یا ارجاع به بخش دیگر تغییر کند
                        CreatedDate = DateTime.Now,
                    };
                    _Entity.Add(Model);
                    await _Context.SaveChangesAsync();
                    FbOut.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, 1, "");
                }
                else
                    FbOut.SetFeedback(ProjectAction.Status, ProjectAction.MessageType, 0, "");

            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.InsertNotSuccess, MessageType.Error, 0, ex.Message);
            }

            return FbOut;
        }
    }
}
