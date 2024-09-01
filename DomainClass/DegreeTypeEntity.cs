using Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
    public class DegreeTypeEntity : BaseEntity<long>
    {

        #region لیست فیلد ها
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        #endregion

        #region لیست ارتباطات
        [InverseProperty(nameof(ProjectActionEntity.DegreeType))]
        public virtual ICollection<ProjectActionEntity> ProjectActions { get; set; } = new List<ProjectActionEntity>();
        #endregion
    }
}
