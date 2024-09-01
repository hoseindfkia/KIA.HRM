using DataLayer;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Project;
using ViewModel.ProjectAction;

namespace Service.ProjectAction
{
    public class ProjectActionService : IProjectActionService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<ProjectActionEntity> _Entity;
        public ProjectActionService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<ProjectActionEntity>();

        }


        /// <summary>
        /// دریافت لیست کارتابل که شامل پروژه هایی است که در کارتابل کاربر می باشد
        /// </summary>
        /// <returns></returns>
        public async Task<Feedback<IList<ProjectActionCartableGetAllViewModel>>> GetAllProjectCartableAsync(ProjectActionStatusType projectActionStatus)
        {
            var FbOut = new Feedback<IList<ProjectActionCartableGetAllViewModel>>();
            try
            {
                var ModelList = await _Entity
                    .Include(p => p.Project)
                    .Include(d => d.DegreeType)
                    .Include(o => o.UserOrigin)
                    .Include(f => f.ProjectFiles)
                    .ThenInclude(f => f.File)
                    .Where(x => x.ProjectActionStatusType == projectActionStatus)
                    .ToListAsync();
                var ViewModelList = ModelList.Select(x => new ProjectActionCartableGetAllViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    DegreeOtherDescription = x.DegreeOtherDescription,
                    DegreeTypeName = x.DegreeType.Title,
                    DegreeTypeTitle = x.DegreeTypeTitle,
                    ProjectActionStatusType = x.ProjectActionStatusType,
                    ProjectName = x.Project.Title,
                    UserCreatorFullName = x.UserOrigin.FirstName + " " + x.UserOrigin.LastName,
                    Files = x.ProjectFiles.Select(f => new ViewModel.File.FileViewModel()
                    {
                        FileName = f.File.FileName,
                        Url = f.File.Url,
                    }).ToList(),

                }).ToList();

                FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, ViewModelList, "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, null, ex.Message);
            }

            return FbOut;
        }

        /// <summary>
        /// ثبت یک اکشن برای یک پروژه
        /// </summary>
        /// <param name="ProjectActionPostViewModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Feedback<int>> AddProjectActionAsycn(ProjectActionPostViewModel ProjectActionPostViewModel)
        {
            var FbOut = new Feedback<int>();
            try
            {

                var Model = new ProjectActionEntity()
                {
                    Title = ProjectActionPostViewModel.Title,
                    Description = ProjectActionPostViewModel.Comment,
                    DegreeOtherDescription = ProjectActionPostViewModel.DegreeOtherDescription,
                    DegreeTypeId = ProjectActionPostViewModel.DegreeTypeId,
                    ProjectId = ProjectActionPostViewModel.ProjectId,
                    DegreeTypeTitle = ProjectActionPostViewModel.DegreeTitle,
                    ProjectActionStatusType = ProjectActionStatusType.Open,
                    UserOriginId = 2,//TODO: باید از توکن خواانده شود
                    CreatedDate = DateTime.Now,
                    ProjectFiles =

                        ProjectActionPostViewModel.Files.Select(x => new ProjectFileEntity()
                        {
                            File = new FileEntity()
                            {
                                FileName = x.FileName,
                                Url = x.Url,
                            }
                        }).ToList()


                };
                var res = _Entity.Add(Model);
                await _Context.SaveChangesAsync();
                FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, 1, "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.InsertNotSuccess, MessageType.Error, 0, ex.Message);
            }

            return FbOut;
        }

        /// <summary>
        /// دریافت تکی 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Feedback<ProjectActionCartableViewModel>> GetProjectCartableAsync(long Id)
        {
            var FbOut = new Feedback<ProjectActionCartableViewModel>();
            try
            {
                var FoundModel = await _Entity
                    .Include(p => p.Project)
                    .Include(d => d.DegreeType)
                    .Include(o => o.UserOrigin)
                    .Include(p => p.ProjectActionAssignUsers)
                    .ThenInclude(u => u.UserAssigned)
                    .Include(p => p.ProjectActionAssignUsers)
                    .ThenInclude(u => u.UserRole.Role)
                    .Include(f => f.ProjectFiles)
                    .ThenInclude(f => f.File)
                    .Where(x => x.Id == Id)
                    .FirstOrDefaultAsync();
                if (FoundModel != null)
                {
                    var ViewModel = new ProjectActionCartableViewModel()
                    {
                        Id = FoundModel.Id,
                        Title = FoundModel.Title,
                        Description = FoundModel.Description,
                        DegreeOtherDescription = FoundModel.DegreeOtherDescription,
                        DegreeTypeName = FoundModel.DegreeType.Title,
                        ProjectActionStatusType = FoundModel.ProjectActionStatusType,
                        ProjectName = FoundModel.Project.Title,
                        DegreeTypeTitle = FoundModel.DegreeTypeTitle,
                        UserCreatorFullName = FoundModel.UserOrigin.FirstName + " " + FoundModel.UserOrigin.LastName,
                        Files = FoundModel.ProjectFiles.Select(f => new ViewModel.File.FileViewModel()
                        {
                            FileName = f.File.FileName,
                            Url = f.File.Url,
                        }).ToList(),
                        ProjectActionAssignUser = FoundModel.ProjectActionAssignUsers.Select(x => new ViewModel.ProjectActionAssignUser.ProjectActionAssignUserListViewModel()
                        {
                            Comment = x.Comment,
                            ProjectActionStatusType = Utility.GetDescriptionOfEnum(typeof(ProjectActionStatusType), x.ProjectActionStatusType),
                            UserFullName = x.UserAssigned.FirstName + " " + x.UserAssigned.LastName,
                            CreateDate = Utility.GregorianDateToPersianCalendar(x.CreatedDate),
                            CreateTime = x.CreatedDate.Hour + ":" + x.CreatedDate.Minute,
                            UserPolicyTitle = x.UserRole.Role.Title,


                        }).ToList(),
                    };

                    FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, ViewModel, "");
                }
                else
                    FbOut.SetFeedback(FeedbackStatus.FileIsNotFound, MessageType.Warninig, null, "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, null, ex.Message);
            }

            return FbOut;
        }

        /// <summary>
        /// تغییر وضعیت پروژه
        /// </summary>
        /// <param name="statusType"></param>
        /// <param name="SaveContext"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> ChangeProjectActionStatusAsync(long ProjectId, ProjectActionStatusType statusType, bool SaveContext = false)
        {
            var FbOut = new Feedback<int>();
            try
            {
                var Model = await _Entity.Where(x => x.Id == ProjectId).FirstOrDefaultAsync();
                if (Model != null)
                {
                    Model.ProjectActionStatusType = statusType;
                    _Entity.Update(Model);
                    if (SaveContext)
                        await _Context.SaveChangesAsync();
                    FbOut.SetFeedback(FeedbackStatus.UpdatedSuccessful, MessageType.Info, 1, "");
                }
                else
                    FbOut.SetFeedback(FeedbackStatus.DataIsNotFound, MessageType.Warninig, 0, "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(FeedbackStatus.InsertNotSuccess, MessageType.Error, 0, ex.Message);
            }

            return FbOut;
        }

       
    }
}
