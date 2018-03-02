using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Model
{
    public class PublishedBook
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfPublication { get; set; }

        public virtual Book Book { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
