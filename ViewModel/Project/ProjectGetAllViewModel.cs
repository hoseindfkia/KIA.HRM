using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Project
{
    public class ProjectGetAllViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }


        public string Description { get; set; }

        public long DegreeTypeId { get; set; }


        public long ProjectStatusId { get; set; }

        public long UserCreatorId { get; set; }

        public string CreatedDate { get; set; }
    }
}
