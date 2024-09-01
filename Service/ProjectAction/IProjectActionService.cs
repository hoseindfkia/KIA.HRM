using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ProjectAction;

namespace Service.ProjectAction
{
    public interface IProjectActionService
    {
        Task<Feedback<IList<ProjectActionCartableGetAllViewModel>>> GetAllProjectCartableAsync(ProjectActionStatusType projectActionStatus);

        Task<Feedback<int>> AddProjectActionAsycn(ProjectActionPostViewModel ProjectActionPostViewModel);

        Task<Feedback<ProjectActionCartableViewModel>> GetProjectCartableAsync(long Id);

        Task<Feedback<int>> ChangeProjectActionStatusAsync(long ProjectId,ProjectActionStatusType statusType,bool SaveContext = false);

      

    }
}
