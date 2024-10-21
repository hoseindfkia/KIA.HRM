using Microsoft.AspNetCore.Mvc;
using Service.CalendarDay;
using Share;
using ViewModel.CalendarDay;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KIA.HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarDayController : ControllerBase
    {
        private readonly ICalendarDayService _calendarDayService;
        public CalendarDayController(ICalendarDayService calendarDayService)
        {
            _calendarDayService = calendarDayService;   
        }



        //[HttpPost("AddDays")]
        //public Task<Feedback<int>> Get(CalenderDayPostViewModel CalenderDay)
        //{
        //    return _calendarDayService.SetDays(CalenderDay);
        //}


        /// <summary>
        /// دریافت روز های یک ماه بر حسب تاریخ ارسالی
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet("GetByMonth")]
        public Task<Feedback<CalendarDayByMonthViewModel>> GetByMonth(DateTime dateTime)
        {
            return _calendarDayService.GetByMonth(dateTime);
        }



    }
}
