using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BrochureViewModel
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж


        public PublishingHouseViewModel PublishingHouse { get; set; }
    }
}
