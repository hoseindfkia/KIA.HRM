using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;

namespace ViewModel.WorkReport.Meeting
{
    public class MeetingPostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDatePersian { get; set; }
        public string ToDatePersian { get; set; }

        public long ProjectId { get; set; }

        public IList<FilePostViewModel> Files { get; set; }


    }
}
