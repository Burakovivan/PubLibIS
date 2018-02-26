using System.Collections.Generic;

namespace PubLibIS_BLL.Model
{
    public class PublishingHouse
    {
        public PublishingHouse()
        {
            Books = new List<Book>();
            Periodicals = new List<Periodical>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public virtual List<Book> Books { get; set; }
        public virtual List<Periodical> Periodicals { get; set; }
    }
}
