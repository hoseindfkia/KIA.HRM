using Microsoft.EntityFrameworkCore;
using Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace DomainClass
{
    public class UserEntity : BaseEntity<long>
    {

        #region لیست فیلد ها
        [Required]
        [StringLength(Constant.StringLengthName)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(Constant.StringLengthName)]
        public string LastName { get; set; }
        [StringLength(Constant.StringLengthName)]
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// نام کاربری با حروف بزرگ ذخیره می شود. به جهت این که زمانی که میخواهیم با بقیه 
        /// یوزر نیم ها مقایسه شود همه با حروف بزرگ ذخیره شود و به دلیل تفاوت حروف بزرگ و کوچک تکراری نشود
        /// </summary>
        [StringLength(Constant.StringLengthName)]
        [Required]
        public string NormalizedEmail { get; set; }
        [MinLength(10), MaxLength(11)]
        public string NationalCode { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [StringLength(Constant.StringLengthEmail)]
        public string Email { get; set; }
        public bool EmailConfirm { get; set; }
        /// <summary>
        /// تعداد دفعات لاگین نا موفق
        /// </summary>
        public int AccessFailedCount { get; set; }
        /// <summary>
        /// تاریخ و ساعتی که اکانت قفل است
        /// </summary>
        [Required]
        public DateTimeOffset LockoutEnd { get; set; }
        #endregion

        #region لیست ارتباطات
        [InverseProperty(nameof(ProjectEntity.UserCreator))]
        public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();

        [InverseProperty(nameof(ProjectActionEntity.UserOrigin))]
        public virtual ICollection<ProjectActionEntity> ProjectActionOrigins { get; set; }

        [InverseProperty(nameof(UserRoleEntity.User))]
        public virtual ICollection<UserRoleEntity> UserRoles  { get; set; }

        [InverseProperty(nameof(ProjectActionAssignUserEntity.UserAssigned))]
        public virtual ICollection<ProjectActionAssignUserEntity> ProjectActionAssignUsers { get; set; }


        #endregion


    }
}
