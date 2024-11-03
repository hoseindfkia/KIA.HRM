using DataLayer;
using DomainClass;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Share;
using Share.Enum;
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

            // صحبتی که شد در صورتی که مرخصی روزانه یا استحقاقی بخورد باید 24 ساعت ثبت می شود اما مرخصی برای کارمند 8 ساعت محاسبه می شود
            TimeOnly startTime = new TimeOnly(0, 0, 0); // 00:00 AM  
            TimeOnly endTime = new TimeOnly(23, 59, 59); // 23:59:59 PM  
            DateTime startDate = new DateTime(LeavePost.FromDate.Year, LeavePost.FromDate.Month, LeavePost.FromDate.Day, startTime.Hour, startTime.Minute, 0);
            DateTime endDate = new DateTime(LeavePost.ToDate.Year, LeavePost.ToDate.Month, LeavePost.ToDate.Day, endTime.Hour, endTime.Minute, endTime.Second);


            var FbOut = new Feedback<int>();
            //TODO: مرخصی نباید تکراری ثبت شود
            var LeaveModel = new LeaveEntity()
            {
                Title = LeavePost.Title,
                Description = LeavePost.Description,
                LeaveType = LeavePost.LeaveType,
                FromDate = ( LeavePost.LeaveType == LeaveType.Daily || LeavePost.LeaveType == LeaveType.Illness) ? startDate : LeavePost.FromDate, // روزانه ها از ساعت تا ساعت ندارد
                ToDate = (LeavePost.LeaveType == LeaveType.Daily || LeavePost.LeaveType == LeaveType.Illness) ? endDate : LeavePost.ToDate,// روزانه ها از ساعت تا ساعت ندارد
                ApproverUserId = null,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsAccepted = null,
                UserCreatorId = null,
            };
            _Entity.Add(LeaveModel);
            await _Context.SaveChangesAsync();
            return FbOut.SetFeedbackNew(FeedbackStatus.UpdatedSuccessful, MessageType.Info, 1, "");
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
                Id = x.Id,  
                Title = x.Title,
                Description = x.Description,
                LeaveType = x.LeaveType,
               // LeaveTypeName = Utility.GetDescriptionOfEnum(typeof(LeaveType), x.LeaveType),
                FromDatePersian = x.FromDate.ToPersianDate(true),
                ToDatePersian = x.ToDate.ToPersianDate(true),
                DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes,
                IsAccepted = x.IsAccepted
            }
            ).AsNoTracking().ToListAsync();
            if (EntityList.Any())
            {
                EntityList = EntityList.Select(x => new LeaveViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    LeaveType = x.LeaveType,
                    LeaveTypeName = Utility.GetDescriptionOfEnum(typeof(LeaveType), x.LeaveType),
                    FromDatePersian = x.FromDatePersian,
                    ToDatePersian = x.ToDatePersian,
                    DurationMinuets = x.DurationMinuets,
                    IsAccepted = x.IsAccepted
                }).ToList();
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, EntityList, "");
            }
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }

        /// <summary>
        /// بررسی همزمان نبود تاریخی که اعلام شده
        /// به جهت آنکه در صورتی که تاریخی تداخل دارد اجازه ثبت ندهد
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> OverlapCheck(DateTime StartDate, DateTime EndDate)
        {
            var Model = await _Entity.Where(x => (x.FromDate < StartDate && x.ToDate > StartDate) ||
                                                  (x.FromDate < EndDate && x.ToDate > EndDate) ||
                                                  (x.FromDate > StartDate && x.ToDate < EndDate)).FirstOrDefaultAsync();
            if (Model == null)
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.FileIsNotFound, Share.Enum.MessageType.Info, 0, "");
            else
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک مرخصی در زمان مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");
        }

        /// <summary>
        /// چک کردن این که آیا در روز جاری مرخصی ثبت شده است یا خیر
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> DuplicateCheck(DateTime StartDate, DateTime EndDate)
        {
            var Model = await _Entity.Where(x => ((x.FromDate < StartDate && x.ToDate > StartDate) ||
                                                 (x.FromDate < EndDate && x.ToDate > EndDate)) &&
                                                 x.FromDate.Date == StartDate.Date ||
                                                 x.FromDate.Date == EndDate.Date ||
                                                 x.ToDate.Date == StartDate.Date ||
                                                 x.ToDate.Date == EndDate.Date).FirstOrDefaultAsync();
            if (Model == null)
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.FileIsNotFound, Share.Enum.MessageType.Info, 0, "");
            else
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک مرخصی در روز مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");
        }
    }
}
