using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    /// <summary>
    /// نوع عملیاتی که کاربر می تواند دسترسی داشته باشد
    /// ( مثال: ایجاد  (مثلا ایجاد پروژه
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// ایجاد
        /// </summary>
        Create = 1,
        /// <summary>
        /// ویرایش
        /// </summary>
        Update = 2,
        /// <summary>
        /// حذف
        /// </summary>
        Delete = 3,
        /// <summary>
        /// ویرایش
        /// </summary>
        View = 4
    }
}
