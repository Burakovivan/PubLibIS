using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PublishedBookViewModel
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        public DateTime DateOfPublication { get; set; }

        public BookViewModel Book { get; set; }
        public PublishingHouseViewModel PublishingHouse { get; set; }
    }
}
