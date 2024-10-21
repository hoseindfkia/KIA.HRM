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

            //TODO:  ساعت باید از ساعت زن خوانده شود
            TimeOnly startTime = new TimeOnly(7, 30, 0); // 07:30 AM  
            TimeOnly endTime = new TimeOnly(17, 0, 0); // 05:00 PM  
            DateTime startDate = new DateTime(LeavePost.FromDate.Year, LeavePost.FromDate.Month, LeavePost.FromDate.Day, startTime.Hour,startTime.Minute,0);
            DateTime endDate = new DateTime(LeavePost.ToDate.Year, LeavePost.ToDate.Month, LeavePost.ToDate.Day, endTime.Hour, endTime.Minute, 0);


            var FbOut = new Feedback<int>();
            //TODO: مرخصی نباید تکراری ثبت شود
            var LeaveModel = new LeaveEntity()
            {
                Title = LeavePost.Title,
                Description = LeavePost.Description,
                LeaveType = LeavePost.LeaveType,
                FromDate = LeavePost.LeaveType == Share.Enum.LeaveType.Daily ? startDate :   LeavePost.FromDate   , // روزانه ها از ساعت تا ساعت ندارد
                ToDate = LeavePost.LeaveType == Share.Enum.LeaveType.Daily ?  endDate : LeavePost.ToDate ,// روزانه ها از ساعت تا ساعت ندارد
                ApproverUserId = null,
                CreatedDate =  DateTime.Now,
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
            // var dateTimeDate = dateTime.dateon;

            var EntityList = await _Entity.Where(x => x.FromDate.Date == dateTime.Date).Select(x => new LeaveViewModel()
            {
                Title = x.Title,
                Description = x.Description,
                LeaveType = x.LeaveType,
                FromDatePersian = x.FromDate.ToPersianDate(true),
                ToDatePersian = x.ToDate.ToPersianDate(true),
                DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes
            }
            ).AsNoTracking().ToListAsync();
            if (EntityList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, EntityList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }
    }
}
