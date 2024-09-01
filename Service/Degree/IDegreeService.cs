using Microsoft.Extensions.Logging;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Degree;
using ViewModel.Project;

namespace Service.Degree
{
    public interface IDegreeService 
    {
        Task<Feedback<IList<DegreeGetAllDropDownViewModel>>> GetAllDropDownAsync();
        Task<Feedback<IList<DegreeGetAllDropDownViewModel>>> Test();

    }
}
