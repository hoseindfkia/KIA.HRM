using Share;
using ViewModel.WorkReport.Leave;

namespace Service.WorkReport.Leave
{
    public interface ILeaveService
    {
        Task<Feedback<int>> AddAsycn(LeavePostViewModel LeavePost, long UserId);
        Task<Feedback<IList<LeaveViewModel>>> GetByDateAsync(DateTime dateTime, long UserId);

        Task<Feedback<int>> OverlapCheck(DateTime StartDate,DateTime EndDate);
        Task<Feedback<int>> DuplicateCheck(DateTime StartDate, DateTime EndDate);
    }
}
