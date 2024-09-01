using DataLayer;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Project;

namespace Service.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<ProjectEntity> _Entity;
        public ProjectService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<ProjectEntity>();
        }
        public async Task<Feedback<IList<ProjectGetAllViewModel>>> GetAllAsync()
        {
            var FbOut = new Feedback<IList<ProjectGetAllViewModel>>();
            try
            {
                var ModelList = await _Entity.AsNoTracking().ToListAsync();

                var ViewModelList = ModelList.Select(x => new ProjectGetAllViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UserCreatorId = x.UserCreatorId,
                    CreatedDate = x.CreatedDate.ToPersianDate(),

                }).ToList();
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, ViewModelList,"");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, null,ex.Message);
            }

            return FbOut;
        }

        /// <summary>
        /// جهت دراپ داون
        /// </summary>
        /// <returns></returns>
        public async Task<Feedback<IList<ProjectGetAllDropDownViewModel>>> GetAllDropDownAsync()
        {
            var FbOut = new Feedback<IList<ProjectGetAllDropDownViewModel>>();
            try
            {
                var ModelList = await _Entity.ToListAsync();

                var ViewModelList = ModelList.Select(x => new ProjectGetAllDropDownViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                }).ToList();
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, ViewModelList, "");
            }
            catch (Exception ex)
            {
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, null, ex.Message);
            }

            return FbOut;
        }
    }
}
