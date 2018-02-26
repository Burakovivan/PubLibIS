using System;

namespace PubLibIS_DAL.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual Author Author { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
