using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    public class LeaveEntity : WorkReportBaseEntity
    {
        #region Fields
        /// <summary>
        /// نوع مرخصی
        ///  </summary>
        public LeaveType LeaveType { get; set; }
        #endregion

        #region Relations
        #endregion
    }
}
