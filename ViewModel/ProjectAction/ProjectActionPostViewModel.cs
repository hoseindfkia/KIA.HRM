using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;

namespace ViewModel.ProjectAction
{
    public class ProjectActionPostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public long ProjectId { get; set; }
        public long? UserDestinationId { get; set; }
       // [Required]
        public string Comment { get; set; }
        [Required]
        public long DegreeTypeId { get; set; }

        public string DegreeTitle { get; set; }
        public string DegreeOtherDescription { get; set; }

        public IList<FileViewModel> Files { get; set; }
        //public IEnumerable<File> files { get; set; }
    }
}
