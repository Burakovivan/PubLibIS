using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS_DAL.Model
{
    public class PublishedBook
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        public DateTime DateOfPublication { get; set; }

        public virtual Book Book { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
