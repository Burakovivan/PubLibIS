using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.ViewModels
{
    public class BackupFileViewModel
    {
        public class BackupFile
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public string User_Id { get; set; }
        }
    }
}
