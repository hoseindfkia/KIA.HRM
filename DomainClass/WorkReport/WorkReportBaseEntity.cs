using Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    /// <summary>
    /// فیلد های مشترک مربوط به گزارش کار
    /// دو حالت رخ می دهد: یا یک جدول طراحی شود و همه فیلدها در یک جدول قرار گیرد و به ازای هر فیلد غیر مشترک یک فیلد در جدول قرار گیرد که در هر بار که یکی 
    /// در جدول قرار می گیرد 3 فیلد غیر مشترک که از جداول دیگر است نال شود. یا اینکه جداول جداگانه در نظر گرفته شود
    /// من روش دوم را انتخاب کردم زیرا ممکن است این فیلد های غیر مشترک در آینده زیادتر شود و فیلد های نال جدول مشترک زیادتر شود
    /// </summary>
    public class WorkReportBaseEntity : BaseEntity<long>
    {
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        [StringLength(Constant.StringLengthDescription)]
        public string Description { get; set; }

        /// <summary>
        /// از تاریخ
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// تا تاریخ
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// تاریخ بروز رسانی
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public long?  UserCreatorId { get; set; }

        /// <summary>
        /// تایید شده یا خیر
        /// </summary>
        public bool? IsAccepted { get; set; }

        /// <summary>
        /// کاربر تایید کننده   
        /// </summary>
        public long? ApproverUserId { get; set; }

    }
}
