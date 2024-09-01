using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ProjectActionAssignUser
{
    public class ProjectActionAssignUserListViewModel
    {
        /// <summary>
        /// کامنتی که کاربر روی پروژه گذاشته است
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// نام و نام خانوادگکگی کاربری که این پروژه به او داده شده است
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// وضعیت پروژه در این حالت
        /// </summary>
        public string ProjectActionStatusType { get; set; }

        public string CreateDate { get; set; }
        public string CreateTime { get; set; }

        public string UserPolicyTitle { get; set; }
    }
}
