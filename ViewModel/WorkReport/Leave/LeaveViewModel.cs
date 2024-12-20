﻿using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WorkReport.Leave
{
    public class LeaveViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDatePersian { get; set; }
        public string ToDatePersian { get; set; }
        public LeaveType LeaveType { get; set; }
        public string LeaveTypeName { get; set; }

        public long  DurationMinuets { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
