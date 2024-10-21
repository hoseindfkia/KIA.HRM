using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WorkReport.PreparationDocument
{
    public class PreparationDocumentViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDatePersian { get; set; }
        public string ToDatePersian { get; set; }
        public int DocumentVersion { get; set; }

        public long DurationMinuets { get; set; }
        //public long DocumentId { get; set; }
    }
}
