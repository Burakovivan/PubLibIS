using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.Domain.Entities
{
    public class BackupFile : BaseEntity
    {
        public string FileNameBase64 { get; set; }
        public string PathToFolder { get; set; }
        public int EncodingCodePage { get; set; }
        public int User_Id { get; set; }

        [ForeignKey("User_Id")]
        [Write(false)]
        public virtual ApplicationUser User{ get; set; }
    }
}
