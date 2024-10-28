using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    public class MeetingEntity : WorkReportBaseEntity
    {
        #region Fields
        public long ProjectId { get; set; }
        #endregion

        #region Relations
        [InverseProperty(nameof(MeetingFileEntity.Meeting))]
        public virtual ICollection<MeetingFileEntity> MeetingFiles { get; set; } = new List<MeetingFileEntity>();

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty(nameof(ProjectEntity.Meetings))]
        public virtual ProjectEntity Project { get; set; }
        #endregion
    }
}
