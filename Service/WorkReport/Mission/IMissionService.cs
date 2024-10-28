using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Meeting;
using ViewModel.WorkReport.Mission;

namespace Service.WorkReport.Mission
{
    public interface IMissionService
    {
        Task<Feedback<int>> AddAsycn(MissionPostViewModel MissionPost, long UserId);

        Task<Feedback<IList<MissionViewModel>>> GetByDate(DateTime dateTime, long UserId);

        Task<Feedback<int>> OverlapCheck(DateTime StartDate, DateTime EndDate);


        Task<Feedback<int>> DuplicateCheck(DateTime StartDate, DateTime EndDate);


    }
}
