using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    public class PreparationDocumentEntity : WorkReportBaseEntity
    {
        #region Fields
        public long DocumentId { get; set; }

        public int DocumentVersion { get; set; }
        #endregion

        #region Relations

        [InverseProperty(nameof(DocumentEntity.PreparationDocuments))]
        [ForeignKey(nameof(DocumentId))]
        public virtual DocumentEntity Document { get; set; }
        #endregion
    }
}
