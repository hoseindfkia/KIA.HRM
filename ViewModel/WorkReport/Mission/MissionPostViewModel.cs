using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;

namespace ViewModel.WorkReport.Mission
{
    public class MissionPostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string FromDatePersian { get; set; }
        public string ToDatePersian { get; set; }
        public long ProjectId { get; set; }
        public MissionType MissionType { get; set; }
        public int CityId { get; set; }

        public IList<FilePostViewModel> Files { get; set; }
    }
}
