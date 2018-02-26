using System;

namespace PubLibIS_DAL.Model
{
    public class Brochure
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }


        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
