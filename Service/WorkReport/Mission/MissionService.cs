using DataLayer;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Service.File;
using Share;
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

            //TODO:  ساعت باید از ساعت زن خوانده شود
            TimeOnly startTime = new TimeOnly(7, 30, 0); // 07:30 AM  
            TimeOnly endTime = new TimeOnly(17, 0, 0); // 05:00 PM  
            DateTime startDate = new DateTime(MissionPost.FromDate.Year, MissionPost.FromDate.Month, MissionPost.FromDate.Day, startTime.Hour, startTime.Minute, 0);
            DateTime endDate = new DateTime(MissionPost.ToDate.Year, MissionPost.ToDate.Month, MissionPost.ToDate.Day, endTime.Hour, endTime.Minute, 0);


            if (Files.Status == Share.Enum.FeedbackStatus.UpdatedSuccessful)

            {
                var MissionModel = new MissionEntity()
                {
                    Title = MissionPost.Title,
                    Description = MissionPost.Description,
                    FromDate = MissionPost.MissionType == Share.Enum.MissionType.Daily ? startDate : MissionPost.FromDate, // روزانه ها از ساعت تا ساعت ندارد
                    ToDate = MissionPost.MissionType == Share.Enum.MissionType.Daily ? endDate : MissionPost.ToDate,// روزانه ها از ساعت تا ساعت ندارد
                    ApproverUserId = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsAccepted = null,
                    UserCreatorId = null,
                    MissionType = MissionPost.MissionType,
                    MissionFiles = Files.Value.Select(file => new MissionFileEntity()
                    {
                        File = file
                    }).ToList(),
                    ProjectId = 0,
                    CityId  = MissionPost.CityId,
                };
                _Entity.Add(MissionModel);
                await _Context.SaveChangesAsync();
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.UpdatedSuccessful, Share.Enum.MessageType.Info, 1, "");
            }
            else
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.CouldNotConnectToServer, Share.Enum.MessageType.Error, 0, "");
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
            var MissionList = await _Entity.Where(x => x.FromDate.Date == dateTime.Date || x.ToDate.Date ==dateTime.Date)
                                          .Select(x => new MissionViewModel
                                          {
                                              Title = x.Title,
                                              Description = x.Description,
                                              FromDatePersian = x.FromDate.ToPersianDate(true),
                                              ToDatePersian = x.ToDate.ToPersianDate(true),
                                              CityName = "",
                                              //Files = x.MeetingFiles.Select(x => new MeetingFileEntity() { 
                                              //}).ToList(),
                                              MissionType = x.MissionType,
                                              DurationMinuets = (long)(x.ToDate - x.FromDate).TotalMinutes
                                          }).AsNoTracking().ToListAsync();
            if (MissionList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, MissionList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }
    }
}
