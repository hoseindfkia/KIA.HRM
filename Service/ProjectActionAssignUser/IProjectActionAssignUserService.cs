using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ProjectAction;
using ViewModel.ProjectActionAssignUser;

namespace Service.ProjectActionAssignUser
{
    public interface IProjectActionAssignUserService
    {
        Task<Feedback<int>> AddProjectActionAssignUserAsycn(ProjectActionAssignUserPostViewModel projectActionAssignUserPostViewModel, ProjectActionStatusType statusType);

       
    }
}
