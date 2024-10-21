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
                DocumentId = 0,
                DocumentVersion = PreparationDocumentPost.DocumentVersion,
            };
            _Entity.Add(PreparationDocumentModel);
            await _Context.SaveChangesAsync();
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, 1, "");
        }

        public async Task<Feedback<IList<PreparationDocumentViewModel>>> GetByDateAsync(DateTime dateTime, long UserId)
        {
            var FbOut = new Feedback<IList<PreparationDocumentViewModel>>();
            var PreparationDocumentList = await _Entity.Where(x => x.FromDate.Date == dateTime.Date)
                                                        .Select(x => new PreparationDocumentViewModel()
                                                        {
                                                            Title = x.Title,
                                                            Description = x.Description,
                                                            FromDatePersian = x.FromDate.ToPersianDate(true),
                                                            ToDatePersian = x.ToDate.ToPersianDate(true),
                                                            DocumentVersion = x.DocumentVersion,
                                                            DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes
                                                        }).AsNoTracking()
                                                       .ToListAsync();
            if (PreparationDocumentList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, PreparationDocumentList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }
    }
}
