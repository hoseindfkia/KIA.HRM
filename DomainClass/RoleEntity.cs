using Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
    public class RoleEntity :BaseEntity<long>
    {

        #region لیست فیلد ها
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        #endregion

        #region لیست ارتباطات
        [InverseProperty(nameof(UserRoleEntity.Role))]    
        public  virtual ICollection<UserRoleEntity> UserRoles  { get; set; }
        #endregion
    }
}
