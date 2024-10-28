using DomainClass.WorkReport;
using Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    public class DocumentEntity : BaseEntity<long>
    {
        #region لیست فیلد ها
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }

        #endregion

        #region لیست ارتباطات

        [InverseProperty(nameof(PreparationDocumentEntity.Document))]
        public virtual ICollection<PreparationDocumentEntity>  PreparationDocuments{ get; set; }

        #endregion

    }
}
