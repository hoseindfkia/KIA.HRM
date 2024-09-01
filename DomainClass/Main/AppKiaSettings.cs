using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Main
{
    public class AppSettings
    {
        public bool IsEncryptionFiles { get; set; }
        public string TemporaryFilePath { get; set; }
        public SFTPAccount FTPServerAccount { get; set; }
    }
}
