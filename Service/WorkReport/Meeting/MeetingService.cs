using DataLayer;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Service.File;
using Share;
using ViewModel.WorkReport.Leave;
using ViewModel.WorkReport.Meeting;
using static System.Net.Mime.MediaTypeNames;

namespace Service.WorkReport.Meeting
{
    public class MeetingService : IMeetingService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<MeetingEntity> _Entity;
        private readonly IFileService _fileService;

        public MeetingService(IUnitOfWorkContext context, IFileService fileService)
        {
            _Context = context;
            _Entity = _Context.Set<MeetingEntity>();
            _fileService = fileService;
        }

        /// <summary>
        /// ذخیره یک جلسه
        /// </summary>
        /// <param name="MeetingPost"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> AddAsycn(MeetingPostViewModel MeetingPost, long UserId)
        {
            var FbOut = new Feedback<int>();
            /// ابتدا فایل های آپلود شده مسیر و بقیه تنظیماتش در دیتابیس ذخیره شود سپس لیست آن در انتیتی ذخیره گردد.
            var Files = await _fileService.AddRangeAsycn(MeetingPost.Files, UserId);
            var MeetingModel = new MeetingEntity()
            {
                Title = MeetingPost.Title,
                Description = MeetingPost.Description,
                FromDate = MeetingPost.FromDate,
                ToDate = MeetingPost.ToDate,
                ApproverUserId = null,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsAccepted = null,
                UserCreatorId = null,
                MeetingFiles =  Files.Value?.Select(file => new MeetingFileEntity()
                {
                    File = file
                }).ToList(),
                ProjectId = MeetingPost.ProjectId
            };
            _Entity.Add(MeetingModel);
            await _Context.SaveChangesAsync();
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, 1, "");

            //if (Files.Value != null)
            //{
            //}
            //else
            //    return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.CouldNotConnectToServer, Share.Enum.MessageType.Error, 0, "");
        }



        /// <summary>
        /// دریافت لیست جلسه های تاریخ ارسالی
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Feedback<IList<MeetingViewModel>>> GetByDateAsync(DateTime dateTime, long UserId)
        {
            var FbOut = new Feedback<IList<MeetingViewModel>>();
            var MeetingList = await _Entity.Include(p=> p.Project).Where(x => x.FromDate.Date == dateTime.Date).Select(x => new MeetingViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                FromDatePersian = x.FromDate.ToPersianDate(true),
                ToDatePersian = x.ToDate.ToPersianDate(true),
                DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes,
                IsAccepted = x.IsAccepted,
                ProjectName = x.Project.Title
                //Files = x.MeetingFiles.Select(x => new MeetingFileEntity() { 

                //}).ToList(),
            }).AsNoTracking().ToListAsync();
            if (MeetingList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, MeetingList, "");
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
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک جلسه در زمان مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");

        }


        /// <summary>
        /// چک کردن این که آیا در روز جاری جلسه ثبت شده است یا خیر
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
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک جلسه در روز مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");
        }
    }
}
