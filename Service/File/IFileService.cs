using DomainClass;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;
using ViewModel.WorkReport.Leave;

namespace Service.File
{
    public interface IFileService
    {
        Task<Feedback<IList<FileEntity>>> AddRangeAsycn(IList<FilePostViewModel> FilePost, long UserId);
    }
}
