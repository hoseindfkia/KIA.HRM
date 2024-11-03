using DataLayer;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Service.File;
using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WorkReport.Meeting;
using ViewModel.WorkReport.Mission;
using static System.Net.Mime.MediaTypeNames;

namespace Service.WorkReport.Mission
{
    public class MissionService : IMissionService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<MissionEntity> _Entity;
        private readonly IFileService _fileService;
        public MissionService(IUnitOfWorkContext context, IFileService fileService)
        {
            _Context = context;
            _Entity = _Context.Set<MissionEntity>();
            _fileService = fileService;
        }


        /// <summary>
        /// ذخیره ماموریت
        /// </summary>
        /// <param name="MissionPost"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public async Task<Feedback<int>> AddAsycn(MissionPostViewModel MissionPost, long UserId)
        {
            var FbOut = new Feedback<int>();
            /// ابتدا فایل های آپلود شده مسیر و بقیه تنظیماتش در دیتابیس ذخیره شود سپس لیست آن در انتیتی ذخیره گردد.
            var Files = await _fileService.AddRangeAsycn(MissionPost.Files, UserId);

            // صحبتی که شد در صورتی که مرخصی روزانه یا استحقاقی بخورد باید 24 ساعت ثبت می شود اما مرخصی برای کارمند 8 ساعت محاسبه می شود
            TimeOnly startTime = new TimeOnly(0, 0, 0); // 00:00 AM  
            TimeOnly endTime = new TimeOnly(23, 59, 59); // 23:59:59 PM  
            DateTime startDate = new DateTime(MissionPost.FromDate.Year, MissionPost.FromDate.Month, MissionPost.FromDate.Day, startTime.Hour, startTime.Minute, 0);
            DateTime endDate = new DateTime(MissionPost.ToDate.Year, MissionPost.ToDate.Month, MissionPost.ToDate.Day, endTime.Hour, endTime.Minute, endTime.Second);

            var MissionModel = new MissionEntity()
            {
                Title = MissionPost.Title,
                Description = MissionPost.Description,
                FromDate = MissionPost.MissionType == MissionType.Daily ? startDate : MissionPost.FromDate,
                ToDate = MissionPost.MissionType == MissionType.Daily ? endDate : MissionPost.ToDate,
                ApproverUserId = null,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsAccepted = null,
                UserCreatorId = null,
                MissionType = MissionPost.MissionType,
                MissionFiles = Files.Value?.Select(file => new MissionFileEntity()
                {
                    File = file
                }).ToList(),
                ProjectId = MissionPost.ProjectId,
                CityId = MissionPost.CityId,
            };
            _Entity.Add(MissionModel);
            await _Context.SaveChangesAsync();
            return FbOut.SetFeedbackNew(FeedbackStatus.UpdatedSuccessful, MessageType.Info, 1, "");
            //if (Files.Status == Share.Enum.FeedbackStatus.UpdatedSuccessful)

            //{
            //}
            //else
            //    return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.CouldNotConnectToServer, Share.Enum.MessageType.Error, 0, "");
        }

        /// <summary>
        /// دریافت لیست ماموریت ها با توجه به تاریخ 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Feedback<IList<MissionViewModel>>> GetByDate(DateTime dateTime, long UserId)
        {
            var FbOut = new Feedback<IList<MissionViewModel>>();
            var MissionList = await _Entity.Include(c => c.City).Where(x => x.FromDate.Date == dateTime.Date || x.ToDate.Date == dateTime.Date)
                                          .Select(x => new MissionViewModel
                                          {
                                              Id = x.Id,
                                              Title = x.Title,
                                              Description = x.Description,
                                              FromDatePersian = x.FromDate.ToPersianDate(true),
                                              ToDatePersian = x.ToDate.ToPersianDate(true),
                                              CityName = x.City.Title,
                                              //Files = x.MeetingFiles.Select(x => new MeetingFileEntity() { 
                                              //}).ToList(),
                                              MissionType = x.MissionType,
                                              //MissionTypeName =  Utility.GetDescriptionOfEnum(typeof(MissionType),x.MissionType),
                                              DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes,
                                              IsAccepted = x.IsAccepted,
                                              ProjectName = x.Project.Title
                                          }).AsNoTracking().ToListAsync();



            if (MissionList.Any())
            {
                // جهت بیرون کشیدن نام نوع شمارشی خطا میداد مجبور شدم دوباره لیست بسازم و مقدارشو پر کنم
                MissionList = MissionList.Select(x => new MissionViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    FromDatePersian = x.FromDatePersian,
                    ToDatePersian = x.ToDatePersian,
                    CityName = x.CityName,
                    MissionType = x.MissionType,
                    MissionTypeName = Utility.GetDescriptionOfEnum(typeof(MissionType), x.MissionType),
                    DurationMinuets = x.DurationMinuets,
                    IsAccepted = x.IsAccepted,
                    ProjectName = x.ProjectName
                }).ToList();

                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, MissionList, "");
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
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک ماموریت در زمان مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");

        }

        /// <summary>
        /// چک کردن این که آیا در روز جاری ماموریت ثبت شده است یا خیر
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
                return new Feedback<int>().SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsIsAvailable, Share.Enum.MessageType.Warninig, 0, "یک ماموریت در روز مورد نظر ثبت شده است. لطفا تاریخ و ساعت را به درستی وارد نماید");
        }

    }
}
