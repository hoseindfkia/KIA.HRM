using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{
    public class FileProperty
    {
        public string FileNewName { get; set; }
        public string FullFileNewName { get; set; }
        public string FileOldName { get; set; }
        public string FileOldPath { get; set; }
        public string Path { get; set; }

        public string FullFileNameLink { get; set; }

        public string PathInServer { get; set; }
        public string PathInServerWithFileName { get; set; }
        public string FullPathFTP { get; set; }

    }
}
