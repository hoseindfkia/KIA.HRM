using Microsoft.AspNetCore.Mvc.ModelBinding;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
    public static class Utility
    {


        /// <summary>
        /// دریافت توضیحات نوع شمارشی
        /// </summary>
        /// <param name="EnumType">نوع شمارشی مورد نظر </param>
        /// <param name="MyValue">مقدار نوع شمارشی </param>
        /// <returns>توضیحات مرتبط با مقدار نوع شمارشی انتخاب شده</returns>
        public static string GetDescriptionOfEnum(object EnumType, object MyValue)
        {
            string DescriptionOfValue = "";
            var ListOfValues = System.Enum.GetValues(EnumType as Type).Cast<object>();
            var valuesAndDescriptions = from ItemEnum in ListOfValues
                                        select new
                                        {
                                            Value = ItemEnum,
                                            Description = ItemEnum.GetType()
                                                .GetMember(ItemEnum.ToString())[0]
                                                .GetCustomAttributes(true)
                                                .OfType<DescriptionAttribute>()
                                                .First()
                                                .Description
                                        };
            var ItemsValue = valuesAndDescriptions.ToArray();
            foreach (var Item in ItemsValue)
            {
                if (((int)Item.Value) == (int)MyValue)
                    DescriptionOfValue = Item.Description;
            }
            if (DescriptionOfValue.Trim() == "")
                DescriptionOfValue = "-";

            return DescriptionOfValue;
        }

        /// <summary>
        /// مقدار و نوع شمارشی داده می شود تا نام انگلیسی نوع شمارشی برگردانده شود
        /// </summary>
        /// <param name="EnumType">نوع شمارشی مورد نظر </param>
        /// <param name="MyValue">مقدار نوع شمارشی </param>
        /// <returns>نام نوع شمارشی مورد نظر</returns>
        public static string GetNameOfEnum(object EnumType, object MyValue)
        {
            return System.Enum.GetName(EnumType as Type, MyValue);
        }


        ///// <summary>
        ///// تبدیل تاریخ میلادی به شمسی
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <returns></returns>
        //public static string GregorianDateToPersianCalendar(DateTime dateTime, bool isTime = false)
        //{
        //    try
        //    {
        //        string persianCalender = "";
        //        PersianCalendar pc = new PersianCalendar();
        //        if (isTime)
        //            persianCalender = string.Format("{0}/{1}/{2}-{3}:{4}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime), pc.GetHour(dateTime), pc.GetMinute(dateTime));
        //        else
        //            persianCalender = string.Format("{0}/{1}/{2}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));
        //        return persianCalender;
        //    }
        //    catch (Exception)
        //    {

        //        return "";
        //    }
        //}


        ///// <summary>
        ///// تبدیل تاریخ میلادی به شمسی
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <returns></returns>
        //public static string ToPersianDate(this DateTime dateTime, bool HasTime = false)
        //{
        //    PersianCalendar persianCalendar = new PersianCalendar();

        //    // Get the Persian year, month, and day  
        //    int year = persianCalendar.GetYear(dateTime);
        //    int month = persianCalendar.GetMonth(dateTime);
        //    int day = persianCalendar.GetDayOfMonth(dateTime);

        //    // Format the date as a string in the desired format (e.g., "YYYY/MM/DD")  
        //    return $"{year}/{month:D2}/{day:D2}" + (HasTime ? "-" + dateTime.Hour + ":" + dateTime.Minute : ""); // D2 formats month and day to two digits  
        //}


        /// <summary>
        /// تبدیل عددهای فارسی به عدد انگلیسی
        /// </summary>
        /// <param ReagentCode=""></param>
        /// <returns></returns>
        public static string ReplacingPerNumberToEnNumber(string ReagentCode)
        {
            if (ReagentCode != null)
            {
                ReagentCode = ReagentCode.Replace('۰', '0');
                ReagentCode = ReagentCode.Replace('۱', '1');
                ReagentCode = ReagentCode.Replace('۲', '2');
                ReagentCode = ReagentCode.Replace('۳', '3');
                ReagentCode = ReagentCode.Replace('۴', '4');
                ReagentCode = ReagentCode.Replace("۵", "5");
                ReagentCode = ReagentCode.Replace("۶", "6");
                ReagentCode = ReagentCode.Replace("۷", "7");
                ReagentCode = ReagentCode.Replace("۸", "8");
                ReagentCode = ReagentCode.Replace("۹", "9");
            }
            else
            {
                return "";
            }
            return ReagentCode;
        }
        /// <summary>
        /// تبدیل رشته ایی به عددی
        /// </summary>
        /// <param name="intString"></param>
        /// <returns></returns>
        public static int ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : 0);
        }




        #region Date Convert
        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToPersianDate(this DateTime dateTime, bool HasTime = false)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            // Get the Persian year, month, and day  
            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            // Format the date as a string in the desired format (e.g., "YYYY/MM/DD")  
            return $"{year}/{month:D2}/{day:D2}" + (HasTime ? "-" + dateTime.Hour + ":" + dateTime.Minute : ""); // D2 formats month and day to two digits  
        }
        /// <summary>
        /// برای این که بتوانیم با تک پارامتری هم کار کنیم
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToPersianDate(this DateTime dateTime)
        {
            return dateTime.ToPersianDate(false);
        }


        /// <summary>
        /// Convert Jalali To Gregorian
        /// تبدیل تاریخ شمسی به میلادی
        /// "1403/07/15 14:30:00"
        /// </summary>
        /// <param name="jalaliDateString"></param>
        /// <returns></returns>
        public static DateTime ToEnglishDateTime(this string jalaliDateString)
        {
            try
            {
                
                // Assuming the input format is "YYYY/MM/DD"  
                var parts = jalaliDateString.Split('-');
                var datePart = parts[0];
                var timePart = parts.Length > 1 ? parts[1] : "00:00:00";


                var dateParts = datePart.Split('/');
               
                int year = int.Parse( dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);

                var timeParts = timePart.Split(':');
                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);
                int second = 0;//int.Parse(timeParts[2]);

                PersianCalendar persianCalendar = new PersianCalendar();
                DateTime gregorianDate = new DateTime(year, month, day, hour, minute, second, persianCalendar);
                return gregorianDate;
            }
            catch (Exception)
            {
                // TODO: خروجی در صورتی که خطا دهد چه باید باشد؟
                return new DateTime();
            }
        }

        #endregion


        #region Date


        public static string GetDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.Shanbeh);
                case DayOfWeek.Sunday:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.YekShanbeh);
                case DayOfWeek.Monday:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.DoShanbeh);
                case DayOfWeek.Tuesday:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.SeShanbeh);
                case DayOfWeek.Wednesday:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.ChaharShanbeh);
                case DayOfWeek.Thursday:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.PanjShanbeh);
                //case DayOfWeek.Friday:
                //    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.Jomee);
                default:
                    return Utility.GetDescriptionOfEnum(typeof(Share.Enum.PersianDayOfWeekType), Share.Enum.PersianDayOfWeekType.Jomee);

            };
        }
        public static PersianDayOfWeekType GetDayOfWeekPersian(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday: return Share.Enum.PersianDayOfWeekType.Shanbeh;
                case DayOfWeek.Sunday: return Share.Enum.PersianDayOfWeekType.YekShanbeh;
                case DayOfWeek.Monday: return Share.Enum.PersianDayOfWeekType.DoShanbeh;
                case DayOfWeek.Tuesday: return Share.Enum.PersianDayOfWeekType.SeShanbeh;
                case DayOfWeek.Wednesday: return Share.Enum.PersianDayOfWeekType.ChaharShanbeh;
                case DayOfWeek.Thursday: return Share.Enum.PersianDayOfWeekType.PanjShanbeh;
                default: return Share.Enum.PersianDayOfWeekType.Jomee;
            };
        }


        public static MonthType GetMonthType(int Month)
        {
            switch (Month)
            {
                case 1:
                    return MonthType.Farvardin;
                case 2:
                    return MonthType.Ordibehesht;
                case 3:
                    return MonthType.Khordad;
                case 4:
                    return MonthType.Tir;
                case 5:
                    return MonthType.Mordad;
                case 6:
                    return MonthType.Shahrivar;
                case 7:
                    return MonthType.Mehr;
                case 8:
                    return MonthType.Aban;
                case 9:
                    return MonthType.Azar;
                case 10:
                    return MonthType.Day;
                case 11:
                    return MonthType.Bahman;
                default:
                    return MonthType.Esfand;
            };
        }
        #endregion


        #region را در خروجی به صورت رشته ایی باز می گرداند ModelState  لیست خطاهای  


        public static string GetModelStateErrors(this ModelStateDictionary ModelState)
        {
            var errors = ModelState.Where(e => e.Value.Errors.Any())
            .SelectMany(e => e.Value.Errors.Select(err => err.ErrorMessage))
            .ToList();
            return errors.Count > 0 ? string.Join(" | ", errors) : string.Empty;
        }


        #endregion




    }
}
