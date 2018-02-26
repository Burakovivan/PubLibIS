using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS_BLL.Model
{
    public class Brochure
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }


        public  PublishingHouse PublishingHouse { get; set; }
    }
}
