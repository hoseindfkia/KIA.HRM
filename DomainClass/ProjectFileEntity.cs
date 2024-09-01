using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    public class ProjectFileEntity :BaseEntity<long>
    {

        #region لیست فیلد ها
        [Required]
        public long FileId { get; set; }
        [Required]
        public long ProjectActionId { get; set; }
        #endregion

        #region لیست ارتباطات
        [ForeignKey(nameof(ProjectActionId))]
        [InverseProperty(nameof(ProjectActionEntity.ProjectFiles))]
        public virtual ProjectActionEntity ProjectAction{ get; set; }

        [ForeignKey(nameof(FileId))]
        [InverseProperty(nameof(FileEntity.ProjectFiles))]
        public virtual FileEntity File{ get; set; }
        #endregion


    }
}
