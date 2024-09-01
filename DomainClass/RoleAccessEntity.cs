using Share;
using Share.Enum;
using System.ComponentModel.DataAnnotations;

namespace DomainClass
{
    public class RoleAccessEntity :BaseEntity<long>
    {

        #region لیست فیلد ها
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        [Required]
        public long RoleId { get; set; }

        [Required]
        public AccessType AccessType { get; set; }

        [Required]
        public ActionType ActionType { get; set; }
        #endregion

        #region لیست ارتباطات
        #endregion
    }
}
