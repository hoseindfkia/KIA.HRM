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
            if (Files.Status == Share.Enum.FeedbackStatus.UpdatedSuccessful)
            {
                var MeetingModel = new MeetingEntity()
                {
                    Title = MeetingPost.Title,
                    Description = MeetingPost.Description,
                    FromDate = MeetingPost.FromDatePersian.ToEnglishDateTime(),
                    ToDate = MeetingPost.ToDatePersian.ToEnglishDateTime(),
                    ApproverUserId = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsAccepted = null,
                    UserCreatorId = null,
                    MeetingFiles = Files.Value.Select(file => new MeetingFileEntity()
                    {
                        File = file
                    }).ToList(),
                    ProjectId = 0
                };
                _Entity.Add(MeetingModel);
                await _Context.SaveChangesAsync();
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, 1, "");
            }
            else
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.CouldNotConnectToServer, Share.Enum.MessageType.Error, 0, "");
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
            var MeetingList = await _Entity.Where(x => x.FromDate == dateTime).Select(x => new MeetingViewModel()
            {
                Title = x.Title,
                Description = x.Description,
                FromDatePersian = x.FromDate.ToPersianDate(),
                ToDatePersian = x.ToDate.ToPersianDate(),
                //Files = x.MeetingFiles.Select(x => new MeetingFileEntity() { 
                
                //}).ToList(),
            }).AsNoTracking().ToListAsync();
            if(MeetingList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, MeetingList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }
    }
}
