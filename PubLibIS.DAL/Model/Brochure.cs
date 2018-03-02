using System;

namespace PubLibIS.DAL.Model
{
    public class Brochure
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж


        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
