using Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainClass;

public class ProjectEntity : BaseEntity<long>
{
    #region لیست فیلد ها

    [Required]
    [StringLength(Constant.StringLengthTitle)]
    public string Title { get; set; }


    [StringLength(Constant.StringLengthDescription)]
    public string Description { get; set; }

 
    [Required]
    public long UserCreatorId { get; set; }
    #endregion

    #region لیست ارتباطات
  
    [InverseProperty(nameof(ProjectActionEntity.Project))]
    public virtual ICollection<ProjectActionEntity> ProjectActions { get; set; } = new List< ProjectActionEntity>();

    [InverseProperty(nameof(UserEntity.Projects))]
    [ForeignKey(nameof(UserCreatorId))]
    public virtual UserEntity UserCreator { get; set; }
    #endregion

}
