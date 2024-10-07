using System;
using System.Collections.Generic;
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
        #endregion
    }
}
