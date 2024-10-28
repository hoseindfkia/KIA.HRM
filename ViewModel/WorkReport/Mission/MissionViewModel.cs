using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;

namespace ViewModel.WorkReport.Mission
{
    public class MissionViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDatePersian { get; set; }
        public string ToDatePersian { get; set; }
        public IList<FileViewModel> Files { get; set; }
        public MissionType MissionType { get; set; }
        public string MissionTypeName { get; set; }
        public string CityName { get; set; }

        public long DurationMinuets { get; set; }
        public bool? IsAccepted { get; set; }
        public string ProjectName { get; set; }
    }
}
