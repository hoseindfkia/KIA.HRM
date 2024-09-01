using Microsoft.AspNetCore.Mvc.ModelBinding;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
    public class Feedback<T>
    {
        public Feedback()
        {
        }
        public Feedback(FeedbackStatus CurrentStatus, MessageType CurrentMessageType, T CurrentValue, string CurrentMessage, string CurrentExceptionMessage)
        {
            Status = CurrentStatus;
            MessageType = CurrentMessageType;
            Value = CurrentValue;
            ExceptionMessage = CurrentExceptionMessage;
        }

        private FeedbackStatus status;
        /// <summary>
        /// برای تعیین وضعیت عملیات انجام شده
        /// </summary>
        public FeedbackStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                Message = Utility.GetDescriptionOfEnum(typeof(FeedbackStatus), value);
            }
        }
        /// <summary>
        /// برای تشخیص warning -info - error
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// یک رکورد مدل یا یک داده
        /// </summary>
        public T Value { get; set; }
        ///// <summary>
        ///// لیستی از رکوردهای مدل
        ///// </summary>
        //public List<T> ValueList { get; set; }
        /// <summary>
        /// پیغام جهت نمایش به کاربر
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// پیغام خطا جهت مشاهده برنامه نویس
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// مشخص می کند پس از انجام عملیات به کدام فرم منتقل شود
        /// </summary>
        public string RedirectTo { get; set; }
        /// <summary>
        /// جهت خروجی خطاهای مربوط به اجباری بودن یا رگولار
        /// </summary>
        public ModelStateDictionary ModelState { get; set; }
        /// <summary>
        /// پر کردن اطلاعات کلاس جهت راحتی برنامه نویس - در صورتی که بخواهیم پیام کاربر را خودمان تعیین کنیم
        /// </summary>
        /// <param name="CurrentStatus">وضعیت عملیات</param>
        /// <param name="CurrentMessageType">تشخیص نوع پیغام</param>
        /// <param name="CurrentValue">مقدار جواب متد</param>
        /// <param name="CurrentValueList">لیستی از جوابها</param>
        /// <param name="CurrentMessage">پیامی که کاربر باید مشاهده کند</param>
        /// <param name="CurrentExceptionMessage">پیام خطا برای برنامه نویس</param>
        public void SetFeedback(FeedbackStatus CurrentStatus, MessageType CurrentMessageType, T CurrentValue, string CurrentMessage, string CurrentExceptionMessage="")
        {
            Status = CurrentStatus;
            MessageType = CurrentMessageType;
            Value = CurrentValue;
            ExceptionMessage = CurrentExceptionMessage;
        }
        /// <summary>
        /// پر کردن اطلاعات کلاس جهت راحتی برنامه نویس - به دلیل اتوماتیک پر شدن پیام کاربر از این متد استفاده می شود
        /// </summary>
        /// <param name="CurrentStatus">وضعیت عملیات</param>
        /// <param name="CurrentMessageType">تشخیص نوع پیغام</param>
        /// <param name="CurrentValue">مقدار جواب متد</param>
        /// <param name="CurrentValueList">لیستی از جوابها</param>        
        /// <param name="CurrentExceptionMessage">پیام خطا برای برنامه نویس</param>
        public void SetFeedback(FeedbackStatus CurrentStatus, MessageType CurrentMessageType, T CurrentValue, string CurrentExceptionMessage)
        {
            Status = CurrentStatus;
            MessageType = CurrentMessageType;
            Value = CurrentValue;
            ExceptionMessage = CurrentExceptionMessage;
        }

        public void SetFeedback(FeedbackStatus CurrentStatus, MessageType CurrentMessageType,  ModelStateDictionary modelState )
        {
            Status = CurrentStatus;
            MessageType = CurrentMessageType;
            ModelState = modelState;
        }

        public Feedback<T> SetFeedbackNew(FeedbackStatus CurrentStatus, MessageType CurrentMessageType, T CurrentValue, string CurrentExceptionMessage)
        {
            var Fbout = new Feedback<T>();
            Fbout.SetFeedback(CurrentStatus, CurrentMessageType, CurrentValue, CurrentExceptionMessage);
            return Fbout;
        }
    }
}
