using DomainClass;
using Service.Project;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Project;

namespace KIA.HRM_Tests.Service.Project
{
    public class ProjectServiceFake : IProjectService
    {
        private readonly List<ProjectGetAllViewModel> _project;
        private readonly List<ProjectGetAllDropDownViewModel> _projectDropDown;
        public ProjectServiceFake()
        {
            _project = new List<ProjectGetAllViewModel>()
            {
                new ProjectGetAllViewModel()
                {
                    Id = 1,
                    Description =  "des 1",
                    Title = "Title1",
                    UserCreatorId = 1,
                    DegreeTypeId = 1,
                    ProjectStatusId = 1,
                } ,new ProjectGetAllViewModel()
                {
                    Id = 2,
                    Description = "des 2",
                    Title = "Title2",
                    UserCreatorId = 2,
                    DegreeTypeId = 1,
                    ProjectStatusId = 1,
                }
            };
            _projectDropDown = new List<ProjectGetAllDropDownViewModel>()
            {
                new ProjectGetAllDropDownViewModel()
                {
                    Id=1,
                    Title = "Title 1"
                },
                new ProjectGetAllDropDownViewModel()
                {
                    Id=2,
                    Title = "Title 2"
                },
            };
        }

        public  async Task<Feedback<IList<ProjectGetAllViewModel>>> GetAllAsync()
        {
            var fbOut =new Feedback<IList<ProjectGetAllViewModel>>();
            fbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, _project, "");
            return  fbOut;
        }

        public async Task<Feedback<IList<ProjectGetAllDropDownViewModel>>> GetAllDropDownAsync()
        {
            var fbOut = new Feedback<IList<ProjectGetAllDropDownViewModel>>();
            fbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, _projectDropDown, "");
            return fbOut;
        }
    }
}
