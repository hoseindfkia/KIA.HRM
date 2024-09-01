using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    /// <summary>
    /// برای انکریپت کردن فایل ها IV   نگه داری کلید و
    /// </summary>
    public class CryptographyEntity : BaseEntity<long>
    {
        #region لیست فیلد ها
        /// <summary>
        ///  کلید انکریپت فایل
        /// </summary>
        [MaxLength(16)]
        public byte[] Key { get; set; }
        /// <summary>
        /// برای انکریپت کردن فایل IV 
        /// </summary>
        [MaxLength(16)]
        public byte[] IV { get; set; }
        /// <summary>
        /// آی دی جدول فایل
        /// </summary>
        public long FileId { get; set; }
        #endregion


        #region ارتباطات
        [ForeignKey(nameof(FileId))]
        [InverseProperty(nameof(FileEntity.Cryptography))]
        public virtual FileEntity File { get; set; }
        #endregion

    }
}
