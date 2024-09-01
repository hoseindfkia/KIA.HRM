using Microsoft.EntityFrameworkCore;
using Share;
using Share.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
    /// <summary>
    /// عملیاتی که روی پروژه انجام می شود. در واقع یک پروژه اصلی به زیر پروژه های یی تبدیل می شود و ممکن است یک پروژه اصلی چندین زیر پروژه داشته باشد
    /// مشخص شده است ProjectActionAssignUserEntity این که به چه کاربراتی اختصاص داه شده است در جدول 
    /// </summary>
    public class ProjectActionEntity : BaseEntity<long>
    {

        #region لیست فیلد ها
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        [Required]
        public long ProjectId { get; set; }
        [Required]
        public long UserOriginId { get; set; }
        [StringLength(Constant.StringLengthDescription)]
        public string Description { get; set; }
        [Required]
        public ProjectActionStatusType ProjectActionStatusType { get; set; }
        public long DegreeTypeId { get; set; }
        [StringLength(Constant.StringLengthDescription)]
        public string DegreeOtherDescription { get; set; } = string.Empty;
        [StringLength(Constant.StringLengthTitle)]
        public string DegreeTypeTitle { get; set; }
        #endregion

        #region لیست ارتباطات
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty(nameof(ProjectEntity.ProjectActions))]
        public virtual ProjectEntity Project { get; set; }

        [InverseProperty(nameof(UserEntity.ProjectActionOrigins))]
        [ForeignKey(nameof(UserOriginId))]
        public virtual UserEntity UserOrigin { get; set; }

        [ForeignKey(nameof(DegreeTypeId))]
        [InverseProperty(nameof(DegreeTypeEntity.ProjectActions))]
        public virtual DegreeTypeEntity DegreeType { get; set; }

        [InverseProperty(nameof(ProjectFileEntity.ProjectAction))]
        public virtual ICollection<ProjectFileEntity> ProjectFiles { get; set; } = new List<ProjectFileEntity>();

        [InverseProperty(nameof(ProjectActionAssignUserEntity.ProjectAction))]
        public virtual ICollection<ProjectActionAssignUserEntity> ProjectActionAssignUsers { get; set; }
        #endregion



    }
}
