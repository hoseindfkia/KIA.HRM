using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace DomainClass
{
    public class UserRoleEntity : BaseEntity<long>
    {
        #region لیست فیلد ها
        [Required]
        public long UserId { get; set; }

        [Required]
        public long RoleId { get; set; }
        #endregion

        #region لیست ارتباطات
        [InverseProperty(nameof(RoleEntity.UserRoles))]
        [ForeignKey(nameof(RoleId))]
        public virtual RoleEntity Role { get; set; }

        [InverseProperty(nameof(UserEntity.UserRoles))]
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }


        [InverseProperty(nameof(ProjectActionAssignUserEntity.UserRole))]
        public virtual ICollection<ProjectActionAssignUserEntity> ProjectActionAssignUsers { get; set; }

        #endregion



    }
}
