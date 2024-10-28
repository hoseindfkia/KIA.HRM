using DomainClass.WorkReport;
using Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainClass;

public class ProjectEntity : BaseEntity<long>
{
    #region Fields

    [Required]
    [StringLength(Constant.StringLengthTitle)]
    public string Title { get; set; }


    [StringLength(Constant.StringLengthDescription)]
    public string Description { get; set; }

 
    [Required]
    public long UserCreatorId { get; set; }
    #endregion

    #region Relations

    [InverseProperty(nameof(ProjectActionEntity.Project))]
    public virtual ICollection<ProjectActionEntity> ProjectActions { get; set; } = new List< ProjectActionEntity>();

    [InverseProperty(nameof(UserEntity.Projects))]
    [ForeignKey(nameof(UserCreatorId))]
    public virtual UserEntity UserCreator { get; set; }

    [InverseProperty(nameof(MeetingEntity.Project))]
    public virtual ICollection<MeetingEntity>  Meetings{ get; set; }


     [InverseProperty(nameof(MissionEntity.Project))]
    public virtual ICollection<MissionEntity>  Missions{ get; set; }



    #endregion

}
