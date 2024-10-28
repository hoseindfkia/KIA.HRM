using DataLayer;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Mission;
using ViewModel.WorkReport.PreparationDocument;

namespace Service.WorkReport.PreparationDocument
{
    public class PreparationDocumentService : IPreparationDocumentService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<PreparationDocumentEntity> _Entity;
        public PreparationDocumentService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<PreparationDocumentEntity>();
        }

        /// <summary>
        /// ذخیره تهیه سند
        /// </summary>
        /// <param name="PreparationDocumentPost"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> AddAsycn(PreparationDocumentPostViewModel PreparationDocumentPost, long UserId)
        {
            var FbOut = new Feedback<int>();
            var PreparationDocumentModel = new PreparationDocumentEntity()
            {
                Title = PreparationDocumentPost.Title,
                Description = PreparationDocumentPost.Description,
                FromDate = PreparationDocumentPost.FromDate,
                ToDate = PreparationDocumentPost.ToDate,
                ApproverUserId = null,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsAccepted = null,
                UserCreatorId = null,
                DocumentId = PreparationDocumentPost.DocumentId,
                DocumentVersion = PreparationDocumentPost.DocumentVersion,
            };
            _Entity.Add(PreparationDocumentModel);
            await _Context.SaveChangesAsync();
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, 1, "");
        }

        public async Task<Feedback<IList<PreparationDocumentViewModel>>> GetByDateAsync(DateTime dateTime, long UserId)
        {
            var FbOut = new Feedback<IList<PreparationDocumentViewModel>>();
            var PreparationDocumentList = await _Entity.Include(d => d.Document).Where(x => x.FromDate.Date == dateTime.Date)
                                                        .Select(x => new PreparationDocumentViewModel()
                                                        {
                                                            Title = x.Title,
                                                            Description = x.Description,
                                                            FromDatePersian = x.FromDate.ToPersianDate(true),
                                                            ToDatePersian = x.ToDate.ToPersianDate(true),
                                                            DocumentVersion = x.DocumentVersion,
                                                            DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes,
                                                            IsAccepted = x.IsAccepted,
                                                            DocumentName = x.Document.Title
                                                        }).AsNoTracking()
                                                       .ToListAsync();
            if (PreparationDocumentList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, PreparationDocumentList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, FbOut.Value, "محتوایی یافت نشد");
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
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک تهیه مدرک در زمان مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");

        }

        /// <summary>
        /// چک کردن این که آیا در روز جاری تهیه مدرک ثبت شده است یا خیر
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
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک تهیه مدرک در روز مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");
        }
    }
}
