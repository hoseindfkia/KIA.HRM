using Share;
using Share.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
    public class FileEntity : BaseEntity<long>
    {

        #region لیست فیلد ها
        [StringLength(Constant.StringLengthName)]
        public string FileName { get; set; }
        [Required]
        [StringLength(Constant.StringLengthURL)]
        public string Url { get; set; }

        /// <summary>
        /// جهت مشخص شدن این که در چه فولدری ذخیره شده است
        /// </summary>
        public FormType FormType { get; set; }

        /// <summary>
        /// نوع فایل
        /// </summary>
        public FileType FileType { get; set; }

        public bool IsEncrypt { get; set; }

        [Required]
        public long UserCreatorId { get; set; }
        /// <summary>
        /// فایل انکریپت شده است یا خیر
        /// انکریپت به صورت اختصاصی است؟ یعنی به ازای هر فایل انکریپت خاص خود تخصیص داده شود
        /// </summary>
        public  bool IsPrivateEncryption { get; }
        /// <summary>
        /// فایل انکریپت است یا خیر؟
        /// انکریپت به صورت عمومی است- یعنی با استفاده از یک کلید عمومی برای انکریپت استفاده شود
        /// در پروژه شیر Constant  موجود در فایل 
        /// </summary>
        public bool IsPublicEncryption { get;  }

      
        #endregion

        #region لیست ارتباطات
        [InverseProperty(nameof(ProjectFileEntity.File))]
        public virtual ICollection<ProjectFileEntity> ProjectFiles { get; set; } = new List<ProjectFileEntity>();

        [InverseProperty(nameof(CryptographyEntity.File))]
        public virtual CryptographyEntity Cryptography { get; set; }

        #endregion
    }
}
