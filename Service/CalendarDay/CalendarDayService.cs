using DataLayer;
using DomainClass;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.CalendarDay;

namespace Service.CalendarDay
{
    public class CalendarDayService : ICalendarDayService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<CalendarDayEntity> _Entity;
        public CalendarDayService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<CalendarDayEntity>();
        }

        /// <summary>
        /// ثبت اولیه تقویم از 10 سال قبل تا 25 سال آینده
        /// </summary>
        /// <param name="CalenderDay"></param>
        /// <returns></returns>
        public async Task<Feedback<int>> SetDays(CalenderDayPostViewModel CalenderDay)
        {
            var FbOut = new Feedback<int>();

            var calendarDays = new List<CalendarDayEntity>();

            for (int year = DateTime.Now.Year - 10; year <= DateTime.Now.Year + 25; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    var daysInMonth = DateTime.DaysInMonth(year, month);

                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        var date = $"{year}/{month:00}/{day:00}";
                        var DateFormat = new DateTime(year, month, day);
                        var persianDate = DateFormat.ToPersianDate();

                        var HasHoliday = CalenderDay.HolidayList.Where(x => x.DatePersian.ToEnglishDateTime() == DateFormat).ToList();

                        bool isHoliday = HasHoliday.Any() || new DateTime(year, month, day).DayOfWeek == DayOfWeek.Friday;

                        var yearPersian = 0;
                        Int32.TryParse(persianDate.Split("/")[0], out yearPersian);
                        var monthPersian = 0;
                        Int32.TryParse(persianDate.Split("/")[1], out monthPersian);
                        var dayPersian = 0;
                        Int32.TryParse(persianDate.Split("/")[2], out dayPersian);

                        calendarDays.Add(new CalendarDayEntity
                        {
                            Year = yearPersian,
                            Month = monthPersian,
                            Day = dayPersian,
                            IsHoliday = isHoliday,
                            // TODO: ممکن است چندین تعطیلی وجود داشته باشد. من اولی را گرفتم
                            HolidayName = isHoliday ? (HasHoliday.Any() ? String.Join(",", HasHoliday.Select(x => x.HolidayName)) : "جمعه") : null,
                            CreatedDate = DateTime.Now,
                            DayPersianName = Utility.GetDayOfWeek(new DateTime(year, month, day).DayOfWeek),
                            MonthType = Utility.GetMonthType(monthPersian),
                            Date = DateFormat,
                            DayOfWeek  = Utility.GetDayOfWeekPersian(new DateTime(year, month, day).DayOfWeek)
                        });
                    }
                }
            }

            _Entity.AddRange(calendarDays);
            await _Context.SaveChangesAsync();


            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.RegisteredSuccessful, Share.Enum.MessageType.Info, 1, "");
        }


        /// <summary>
        /// دریافت یک ماه بر حسب تاریخ ورودی
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public async Task<Feedback<CalendarDayByMonthViewModel>> GetByMonth(DateTime dateTime)
        {
            var FbOut = new Feedback<CalendarDayByMonthViewModel>();
            var CalendarDayByMonth = new CalendarDayByMonthViewModel();   
            var DaysList = await _Entity.Where(x => x.Date.Year == dateTime.Year && x.Date.Month == dateTime.Month)
                                        .Select(x => new CalendarDayViewModel()
                                        {
                                            Id = x.Id,
                                            Year = x.Date.Year,
                                            Month = x.Date.Month,
                                            Day = x.Date.Day,
                                            Date = x.Date,
                                            DayPersianName = x.DayPersianName,
                                            HolidayName = x.HolidayName,
                                            IsHoliday = x.IsHoliday,
                                            MonthType = x.MonthType,
                                            DayOfWeek = x.DayOfWeek
                                           // MonthName = Utility.GetDescriptionOfEnum(typeof(MonthType), x.MonthType)
                                        })
                                        .ToListAsync();
            foreach (var Day in DaysList) {
                var enumType = typeof(MonthType);
                Day.MonthName = Utility.GetDescriptionOfEnum(enumType,Day.MonthType);
            }
            CalendarDayByMonth.Days = DaysList;
            if (DaysList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, CalendarDayByMonth, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");
        }


    }
}
