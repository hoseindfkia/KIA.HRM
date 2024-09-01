using Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.File;
using ViewModel.ProjectActionAssignUser;

namespace ViewModel.ProjectAction
{
    public class ProjectActionCartableViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ProjectName { get; set; }
        public string UserCreatorFullName { get; set; }
        public string Description { get; set; }
        public ProjectActionStatusType ProjectActionStatusType { get; set; }
        public string DegreeTypeName { get; set; }
        public string DegreeTypeTitle { get; set; }
        public string DegreeOtherDescription { get; set; } = string.Empty;
        public IList<FileViewModel> Files { get; set; }

        public IList<ProjectActionAssignUserListViewModel> ProjectActionAssignUser { get; set; }
    }
}
