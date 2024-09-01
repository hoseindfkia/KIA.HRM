using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ProjectActionAssignUser
{
    public class ProjectActionAssignUserPostViewModel
    {
        public long ProjectActionId { get; set; }
        public long UserAssignedId { get; set; }
        public long UserRoleId { get; set; }
        public string Comment { get; set; }
    }
}
