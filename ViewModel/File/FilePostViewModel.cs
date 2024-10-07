using Share;
using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.File
{
    public class FilePostViewModel
    {
        [StringLength(Constant.StringLengthName)]
        public string FileName { get; set; }
        [Required]
        [StringLength(Constant.StringLengthURL)]
        public string Url { get; set; }
       
        public FormType FormType { get; set; }
       
        public FileType FileType { get; set; }

        //public bool IsEncrypt { get; set; }

        //[Required]
        //public long UserCreatorId { get; set; }
      
        //public bool IsPrivateEncryption { get; }
       
        //public bool IsPublicEncryption { get; }

        //public byte[] EncryptionKey { get; set; }

    }
}
