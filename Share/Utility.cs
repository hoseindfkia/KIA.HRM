﻿using System;
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


        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GregorianDateToPersianCalendar(DateTime dateTime, bool isTime = false)
        {
            try
            {
                string persianCalender = "";
                PersianCalendar pc = new PersianCalendar();
                if (isTime)
                    persianCalender = string.Format("{0}/{1}/{2}-{3}:{4}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime), pc.GetHour(dateTime), pc.GetMinute(dateTime));
                else
                    persianCalender = string.Format("{0}/{1}/{2}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));
                return persianCalender;
            }
            catch (Exception)
            {

                return "";
            }
        }


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


    }
}