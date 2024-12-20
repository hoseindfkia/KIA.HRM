﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;

namespace ViewModel.WorkReport.Meeting
{
    public class MeetingViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDatePersian { get; set; }
        public string ToDatePersian { get; set; }

        public IList<FileViewModel> Files { get; set; }

        public long DurationMinuets { get; set; }

        public bool? IsAccepted { get; set; }

        public string ProjectName { get; set; }

    }
}
