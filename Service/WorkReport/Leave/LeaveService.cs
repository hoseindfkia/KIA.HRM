using DataLayer;
using DomainClass;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Leave;

namespace Service.WorkReport.Leave
{
    public class LeaveService : ILeaveService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<LeaveEntity> _Entity;
        public LeaveService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<LeaveEntity>();
        }

        /// <summary>
        /// ذخیره مرخصی
        /// </summary>
        /// <param name="LeavePost"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> AddAsycn(LeavePostViewModel LeavePost, long UserId)
        {
            var FbOut = new Feedback<int>();
            var LeaveModel = new LeaveEntity()
            {
                Title = LeavePost.Title,
                Description = LeavePost.Description,
                LeaveType = LeavePost.LeaveType,
                FromDate = LeavePost.FromDatePersian.ToEnglishDateTime(),
                ToDate = LeavePost.ToDatePersian.ToEnglishDateTime(),
                ApproverUserId = null,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsAccepted = null,
                UserCreatorId = null,
            };
            _Entity.Add(LeaveModel);
            await _Context.SaveChangesAsync();
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, 1, "");
        }

        /// <summary>
        /// دریافت مرخصی بر حسب تاریخ
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Feedback<IList<LeaveViewModel>>> GetByDateAsync(DateTime dateTime, long UserId)
        {
            var FbOut = new Feedback<IList<LeaveViewModel>>();
            var EntityList = await _Entity.Where(x=> x.FromDate == dateTime).Select(x => new LeaveViewModel()
            {
                Title = x.Title,
                Description = x.Description,
                LeaveType = x.LeaveType,
                FromDatePersian = x.FromDate.ToPersianDate(),
                ToDatePersian = x.ToDate.ToPersianDate(),
            }
            ).AsNoTracking().ToListAsync();
            if (EntityList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, EntityList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }
    }
}
