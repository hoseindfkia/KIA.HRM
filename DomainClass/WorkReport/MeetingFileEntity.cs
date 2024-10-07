using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    public class MeetingFileEntity : BaseEntity<long>
    {

        #region Fields
        public long FileId { get; set; }
        public long  MeetingId { get; set; }

        #endregion

        #region Relations
        [InverseProperty(nameof(FileEntity.MeetingFiles))]
        [ForeignKey(nameof(FileId))]
        public virtual FileEntity File { get; set; }

        [InverseProperty(nameof(MeetingEntity.MeetingFiles))]
        [ForeignKey(nameof(MeetingId))]
        public virtual MeetingEntity Meeting { get; set; }
        #endregion
    }
}
