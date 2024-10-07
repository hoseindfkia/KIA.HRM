using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    public class MissionFileEntity : BaseEntity<long>
    {

        #region Fields
        public long FileId { get; set; }
        public long MissionId { get; set; }
        #endregion

        #region Relations
        [InverseProperty(nameof(FileEntity.MissionFiles))]
        [ForeignKey(nameof(FileId))]
        public virtual FileEntity File { get; set; }

        [InverseProperty(nameof(MissionEntity.MissionFiles))]
        [ForeignKey(nameof(MissionId))]
        public virtual MissionEntity Mission { get; set; }
        #endregion
    }
}
