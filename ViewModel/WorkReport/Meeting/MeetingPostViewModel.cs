﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Range(typeof(DateTime), "1/2/2000", "3/4/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime FromDate { get; set; }
        [Range(typeof(DateTime), "1/2/2000", "3/4/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime ToDate { get; set; }
        public long ProjectId { get; set; }

        public IList<FilePostViewModel> Files { get; set; }


    }
}
