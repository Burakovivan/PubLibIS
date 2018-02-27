using System;
using System.Collections.Generic;

namespace PubLibIS_DAL.Model
{
    public class PublishingHouse
    {
        public PublishingHouse()
        {
            Books = new List<Book>();
            Periodicals = new List<Periodical>();
            Brochures = new List<Brochure>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public DateTime FoundationDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Periodical> Periodicals { get; set; }
        public virtual ICollection<Brochure> Brochures { get; set; }
    }
}
