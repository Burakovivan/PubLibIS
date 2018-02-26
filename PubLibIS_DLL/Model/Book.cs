using System;

namespace PubLibIS_BLL.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }

        public  Author Author { get; set; }
        public  PublishingHouse PublishingHouse { get; set; }
    }
}
