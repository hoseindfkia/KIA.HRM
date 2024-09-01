using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Project;

namespace Service.Project
{
    public interface IProjectService
    {
        Task<Feedback<IList<ProjectGetAllViewModel>>> GetAllAsync();
        Task<Feedback<IList<ProjectGetAllDropDownViewModel>>> GetAllDropDownAsync();
    }
}
