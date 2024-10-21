using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WorkReport.Leave
{
    public class LeavePostViewModel
    {
        [Required(ErrorMessage ="عنوان الزامی است")]
        [MinLength(3,ErrorMessage ="حداقل طول عنوان 3 کاراکتر می باشد")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(typeof(DateTime), "1/2/2000", "3/4/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime FromDate { get; set; }
        [Range(typeof(DateTime), "1/2/2000", "3/4/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime ToDate { get; set; }

        public LeaveType LeaveType { get; set; }
    }
}
