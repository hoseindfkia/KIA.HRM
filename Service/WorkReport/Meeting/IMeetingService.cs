using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Meeting;

namespace Service.WorkReport.Meeting
{
    public interface IMeetingService
    {
        Task<Feedback<int>> AddAsycn(MeetingPostViewModel MeetingPost, long UserId);
        Task<Feedback<IList<MeetingViewModel>>> GetByDateAsync(DateTime dateTime, long UserId);

        Task<Feedback<int>> OverlapCheck(DateTime StartDate, DateTime EndDate);

        Task<Feedback<int>> DuplicateCheck(DateTime StartDate, DateTime EndDate);


    }
}
