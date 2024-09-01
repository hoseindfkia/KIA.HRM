using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    /// <summary>
    /// این انتیتی مشخص می کند هر پروژه به چه کاربری اختصاص داده می شود و چه مواردی به پروژه اضافه می کند. شامل کامنت گذاشتن روی پروژه یا عملیات اقدام یا ارسال به بخش دیگر
    /// </summary>
    public class ProjectActionAssignUserEntity : BaseEntity<long>
    {
        #region لیست فیلد ها
        /// <summary>
        /// پروژه اکشن
        /// </summary>
        public long ProjectActionId { get; set; }
        /// <summary>
        /// کاربری که به او اختصاص داده شده است
        /// </summary>
        public long UserAssignedId { get; set; }

        /// <summary>
        /// با چه سمتی  از کاربر به او اختصاص داده شده است
        /// </summary>
        public long UserRoleId { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// وضعیت پروژه توسط هر کاربر
        /// </summary>
        public ProjectActionStatusType ProjectActionStatusType { get; set; }

        #endregion
        #region لیست ارتباطات

        [InverseProperty(nameof(ProjectActionEntity.ProjectActionAssignUsers))]
        [ForeignKey(nameof(ProjectActionId))]
        public virtual ProjectActionEntity ProjectAction { get; set; }

        [InverseProperty(nameof(UserEntity.ProjectActionAssignUsers))]
        [ForeignKey(nameof(UserAssignedId))]
        public virtual  UserEntity UserAssigned { get; set; }

        [InverseProperty(nameof(UserRoleEntity.ProjectActionAssignUsers))]
        [ForeignKey(nameof(UserRoleId))]
        public virtual  UserRoleEntity UserRole { get; set; }
        #endregion
    }
}
